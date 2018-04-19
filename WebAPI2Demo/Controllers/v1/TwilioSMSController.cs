using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Xml.Linq;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;
using WebAPI2Demo.TheBusiness;
using WebAPI2Demo.TheData;

namespace WebAPI2Demo.Controllers.v0
{
    [RoutePrefix("api/twilio/sms/v1")]
    public class V1TwilioSMSController : ApiController
    {
        [Route("reply")]
        [HttpPost]
        //[ValidateRequest("fed5fd7f0b358488325d9b6216ba5845")]
        public HttpResponseMessage TwilioSMS([FromBody]SmsRequest request)
        {
            var responseString = SMSParsing.ParseSmsMessage(request);
            
            if(string.IsNullOrEmpty(responseString))
            {
                //TODO: Not sure how to correctly send no response, in case I don't want to respond to a command?
                //this may throw an error on Twilio's side
                return null;
            }

            var response = new MessagingResponse();
            response.Message(responseString);

            return Request.CreateResponse(HttpStatusCode.OK, XElement.Parse(response.ToString()), new XmlMediaTypeFormatter());
        }

        private void LogRequest(SmsRequest request)
        {
            var success = SMSHelper.SendSmsMessage(new List<string> { "+16146538779" }, request.Body + "\nFrom: " + request.From);
        }
    }
}
