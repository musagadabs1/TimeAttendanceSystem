﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TimeAttendanceSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.ErrorCorrection> ErrorCorrections { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.Compile> Compiles { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.ManualEntry> ManualEntries { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.RollBack> RollBacks { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.Individual> Individuals { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.AttendanceMail> AttendanceMails { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.Error> Errors { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.EntryCheck> EntryChecks { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.EditEntry> EditEntries { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.Finalize> Finalizes { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.Absentee> Absentees { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.Presence> Presences { get; set; }

        public System.Data.Entity.DbSet<TimeAttendanceSystem.Models.DailyInOutReport> DailyInOutReports { get; set; }

        //object placeHolderVariable;
        //object placeHolderVariable;
    }
}