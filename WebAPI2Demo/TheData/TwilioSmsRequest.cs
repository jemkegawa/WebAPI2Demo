using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI2Demo.TheData
{
    public class TwilioSmsRequest
    {
        public string MessageSid { get; set; }
        public string AccountSid { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
}