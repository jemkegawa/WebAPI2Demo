using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI2Demo.TheData
{
    public class WebAPIDemoRole
    {
        public enum Role { NotSet, User, Admin }

        public int Id { get; set; }
        public Role UserRole { get; set; }
    }
}