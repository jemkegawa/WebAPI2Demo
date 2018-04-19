using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI2Demo.TheData
{
    public class WebAPIDemoContest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? GoLiveTime { get; set; }
        public ICollection<WebAPIDemoSMS> PeopleEnrolled { get; set; }
    }
}