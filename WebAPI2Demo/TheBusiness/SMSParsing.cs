using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio.AspNet.Common;
using WebAPI2Demo.TheData;

namespace WebAPI2Demo.TheBusiness
{
    public static class SMSParsing
    {
        static WebAPIDemoSMS PhoneInDb;
        static string ResponseString;

        static SMSParsing()
        {
            ResponseString = string.Empty;
        }

        public static string ParseSmsMessage(SmsRequest request)
        {
            var actionString = request.Body.Split(' ').FirstOrDefault().Replace("\"", "");
            var values = String.Join(" ", (request.Body.Split(' ')).Skip(1));

            if (!IsPhoneRegistered(request.From) && actionString.ToLower() != "join" && actionString.ToLower() != "game" && actionString.ToLower() != "win")
            {
                PhoneInDb = null;
                return SMSResponseMessages.UnknownPhoneMessage(request.From);
            }

            if (IsPhoneRegistered(request.From) && actionString.ToLower() == "join")
            {
                PhoneInDb = null;
                return SMSResponseMessages.AlreadyRegisteredMessage(request.From);
            }

            ParseSMSRequestAction(request, actionString.ToLower(), values);

            PhoneInDb = null;

            return ResponseString;
        }

        private static void ParseSMSRequestAction(SmsRequest request, string actionString, string values)
        {
            //accepted command list (actions)
            switch (actionString.ToLower())
            {
                case "game":
                    //values should contain the person's name in this case
                    ResponseString = SMSContest.Signup(request, values);
                    break;
                case "win":
                    ResponseString = SMSContest.Compete(request, values);
                    break;


                //SMS "Bot" commands
                case "join":
                    DoJoinAction(request, values);
                    break;
                case "email":
                    DoEmailAction(request, values);
                    break;
                case "joke":
                    ResponseString = SMSResponseMessages.Joke(request.From);
                    break;
                case "cat":
                    ResponseString = SMSResponseMessages.NotImplementedMessage(request.From);
                    break;
                case "awoo":
                case "aw00":
                case "awooo":
                case "awoooo":
                case "awoo!":
                case "aw00!":
                case "awooo!":
                case "awoooo!":
                    ResponseString = SMSResponseMessages.Woof(request.From);
                    break;
                case "error":
                    //just so I can see the error message sends correctly :)
                    ResponseString = SMSResponseMessages.ErrorMessage(request.From);
                    break;
                case "stop":
                case "unregister":
                    UnregisterPerson(request.From);
                    break;
                default:
                    ResponseString = SMSResponseMessages.UnknownCommandMessage(request.From);
                    break;
            }
        }





        /// <summary>
        /// Joins the SMS Bot
        /// </summary>
        /// <param name="request"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private static void DoJoinAction(SmsRequest request, string values)
        {
            if (values == string.Empty)
            {
                ResponseString = SMSResponseMessages.MissingValueMessage(request.From, "name", "join Jem Systems");
            }
            else
            {
                var registered = RegisterPhone(request.From, values);
                if (registered)
                    ResponseString = SMSResponseMessages.JustRegisteredMessage(request.From);
                else
                    ResponseString = SMSResponseMessages.ErrorMessage(request.From);
            }
        }

        /// <summary>
        /// Registers an email with the SMS Bot
        /// </summary>
        /// <param name="request"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private static void DoEmailAction(SmsRequest request, string values)
        {
            if (values == string.Empty)
                ResponseString = SMSResponseMessages.MissingValueMessage(request.From, "email", "email service@jemsystemsllc.com");
            else
            {
                var emailUpdated = UpdateEmail(request.From, values);
                if (emailUpdated)
                    ResponseString = SMSResponseMessages.CommandsMessage(request.From);
                else
                    ResponseString = SMSResponseMessages.ErrorMessage(request.From);
            }
        }

        /// <summary>
        /// Returns if a phone is registered already
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        private static bool IsPhoneRegistered(string phoneNumber)
        {
            try
            {
                if (GetPhoneEnrollment(phoneNumber) == null)
                    return false;

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the database for phone enrollment with SMS Bot
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static WebAPIDemoSMS GetPhoneEnrollment(string phoneNumber)
        {
            if (PhoneInDb != null)
                return PhoneInDb;

            var phoneInDb = SMSDataHelper.GetPhoneEnrollment(phoneNumber);

            PhoneInDb = phoneInDb;
            return phoneInDb;
        }

        /// <summary>
        /// Does a sql update through entity of the email address
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        private static bool UpdateEmail(string phoneNumber, string email)
        {
            return SMSDataHelper.UpdateEmail(phoneNumber, email);
        }

        /// <summary>
        /// Registers a person's phone information through entity
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        private static bool RegisterPhone(string phoneNumber, string name, string email = "")
        {
            return SMSDataHelper.RegisterPhone(phoneNumber, name, email) != null;
        }

        /// <summary>
        /// Does an update to unregister a phone by changing the phone number field (keeping the record for history)
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        private static string UnregisterPerson(string phoneNumber)
        {
            return SMSDataHelper.UnregisterPerson(phoneNumber);
        }
    }
}