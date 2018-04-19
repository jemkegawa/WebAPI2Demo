using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Twilio.AspNet.Common;
using WebAPI2Demo.TheData;

namespace WebAPI2Demo.TheBusiness
{
    public static class SMSContest
    {
        private static WebAPIDemoContest _contest;
        
        public static string Signup(SmsRequest request, string values)
        {
            var responseString = string.Empty;

            if(string.IsNullOrEmpty(values))
            {
                return SMSResponseMessages.MissingValueMessage(request.From, "name", "game Awesome Person #5");
            }

            return RegisterPhoneForContest(request.From, values);
        }
        
        public static string Compete(SmsRequest request, string values)
        {
            return SetContestEntryAndGetResponse(request.From, values);
        }

        public static WebAPIDemoContest SetContestStart(int contestId, DateTime contestStart)
        {
            var contest = GetContestWithPeople(contestId);

            if (contest == null)
                return null;

            try
            {
                using (var db = new WebAPIDemoSMSContestContext())
                {
                    contest.GoLiveTime = contestStart;
                    db.Contests.Attach(contest);
                    db.Entry(contest).State = EntityState.Modified;
                    db.SaveChanges();

                    return contest;
                }
            }
            catch
            {
                return null;
            }
        }

        public static void SendTexts(WebAPIDemoContest contest)
        {
            var phones = new List<string>();

            foreach(var personEnrolled in contest.PeopleEnrolled)
            {
                phones.Add(personEnrolled.Phone);
            }

            SMSHelper.SendSmsMessage(phones, "The " + contest.Name + " contest has started! Reply WIN for your chance to win!");
        }
        
        private static WebAPIDemoContest GetContest(string contestName)
        {
            if (_contest != null && _contest.Name == contestName)
                return _contest;

            using (var db = new WebAPIDemoSMSContestContext())
            {
                var contestInDb = db.Contests
                    .Where(c => c.Name == contestName)
                    .SingleOrDefault();

                if(contestInDb != null)
                    _contest = contestInDb;

                return contestInDb;
            }
        }

        private static string RegisterPhoneForContest(string phoneNumber, string name, string email = "")
        {
            var contest = GetContestWithPeople(1);

            var demoSMS = contest.PeopleEnrolled.FirstOrDefault(e => e.Phone == phoneNumber);

            if (demoSMS == null)
            {
                demoSMS = SMSDataHelper.GetPhoneEnrollment(phoneNumber);

                if(demoSMS == null)
                    demoSMS = SMSDataHelper.RegisterPhone(phoneNumber, name, email);

                if (demoSMS == null)
                    return "There was an error enrolling you in the contest. Your phone/name could not be added.";


                try
                {
                    using (var db = new WebAPIDemoSMSContestContext())
                    {
                        db.Enrollments.Attach(demoSMS);
                        db.SaveChanges();

                        db.Contests.Attach(contest);
                        contest.PeopleEnrolled.Add(demoSMS);

                        db.SaveChanges();
                    }
                }
                catch(Exception ex)
                {
                    return "There was an error enrolling you in the contest: " + (ex.Message.Length > 80 ? ex.Message.Substring(0, 80) : ex.Message) + "...";
                }
                
                return "You're now signed up to play! When you see the text that the contest has begun, reply first with the contest word given for your chance to win!";
            }

            return "Looks like you're already signed up for the contest! Once the key word has been sent out, be first to respond to win!";
        }

        private static string SetContestEntryAndGetResponse(string phoneNumber, string name)
        {
            var contest = GetContestWithPeople(1);

            var demoSMS = contest.PeopleEnrolled.FirstOrDefault(e => e.Phone == phoneNumber);

            if (demoSMS == null)
            {
                return "Sorry, but it looks like you weren't signed up for the contest... How did you even get the code? :P";
            }

            var sqlDateMinValue = DateTime.Parse("1900-01-01");

            //if not already competed, update to current time
            if (demoSMS.ContestDate == null || demoSMS.ContestDate == sqlDateMinValue)
            {
                demoSMS.ContestDate = DateTime.Now;

                try
                {
                    using (var db = new WebAPIDemoSMSContestContext())
                    {
                        db.Enrollments.Attach(demoSMS);
                        db.Entry(demoSMS).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            var winTime = demoSMS.ContestDate - contest.GoLiveTime;
            var position = 1;

            foreach(var enrollment in contest.PeopleEnrolled)
            {
                if (enrollment.Phone == demoSMS.Phone)
                    continue;

                if (enrollment.ContestDate < demoSMS.ContestDate && enrollment.ContestDate != null && enrollment.ContestDate != sqlDateMinValue)
                    position++;
            }

            if (position == 1)
                return "Congratulations! Come see Christopher/Sara now to claim your prize! (Must be present to win - prizes given by quickest to respond!)";
            
            return "Thanks for playing! " + (position-1) + " " + (position==2 ? "person" : "people") + " answered before you.";
        }

        private static WebAPIDemoContest GetContestWithPeople(int contestId)
        {
            try
            {
                using (var db = new WebAPIDemoSMSContestContext())
                {
                    var contest = db.Contests
                        .Include(c => c.PeopleEnrolled)
                        .Where(c => c.Id == 1)
                        .SingleOrDefault();

                    return contest;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}