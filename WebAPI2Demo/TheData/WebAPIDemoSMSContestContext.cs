using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;

namespace WebAPI2Demo.TheData
{
    public partial class WebAPIDemoSMSContestContext : DbContext
    {
        public DbSet<WebAPIDemoSMS> Enrollments { get; set; }
        public DbSet<WebAPIDemoRole> Roles { get; set; }
        public DbSet<WebAPIDemoContest> Contests { get; set; }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}