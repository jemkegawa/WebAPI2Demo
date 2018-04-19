using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI2Demo.TheData;

namespace WebAPI2Demo.TheBusiness
{
    public static class SMSDataHelper
    {
        public static WebAPIDemoSMS GetPhoneEnrollment(string phoneNumber)
        {
            using (var db = new WebAPIDemoSMSContestContext())
            {
                var phoneInDb = db.Enrollments
                    .Where(p => p.Phone == phoneNumber)
                    .FirstOrDefault();
                return phoneInDb;
            }
        }
        
        public static bool UpdateEmail(string phoneNumber, string email)
        {
            var phoneInDb = GetPhoneEnrollment(phoneNumber);

            if (phoneInDb == null)
                return false;

            try
            {
                using (var db = new WebAPIDemoSMSContestContext())
                {
                    phoneInDb.Email = email;
                    phoneInDb.UpdatedDate = DateTime.Now;
                    db.Enrollments.Attach(phoneInDb);
                    db.Entry(phoneInDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        
        public static WebAPIDemoSMS RegisterPhone(string phoneNumber, string name, string email = "")
        {
            using (var db = new WebAPIDemoSMSContestContext())
            {
                var enrollment = new WebAPIDemoSMS { Name = name, Phone = phoneNumber, CreationDate = DateTime.Now, UpdatedDate = DateTime.Now, Email = (email == "" ? null : email) };

                try
                {
                    db.Enrollments.Add(enrollment);
                    db.SaveChanges();

                    return enrollment;
                }
                catch
                {
                    return null;
                }
            }
        }
        
        public static string UnregisterPerson(string phoneNumber)
        {
            //yea, yea.. there should probably be a flag in this database like 'active' that determines if they're registered... or even a history table that stores the old info so we can delete it from here, but for now this works and I don't lose who has used the system :)
            //large reason for now for saving who has used it - if someone abuses it and starts costing me lots of money, I can track that down easier

            var phoneInDb = GetPhoneEnrollment(phoneNumber);

            if (phoneInDb == null)
                return SMSResponseMessages.UnknownPhoneMessage(phoneNumber);

            try
            {
                using (var db = new WebAPIDemoSMSContestContext())
                {
                    phoneInDb.Phone = "Unregistered-" + phoneInDb.Phone;
                    phoneInDb.UpdatedDate = DateTime.Now;
                    db.Enrollments.Attach(phoneInDb);
                    db.Entry(phoneInDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return SMSResponseMessages.UnregisteredMessage(phoneNumber);
                }
            }
            catch
            {
                return SMSResponseMessages.ErrorMessage(phoneNumber);
            }
        }
    }
}