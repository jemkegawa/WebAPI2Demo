using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI2Demo.TheData
{
    public class WebAPIDemoSMS
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public WebAPIDemoRole Role { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? ContestDate { get; set; }
    }
}