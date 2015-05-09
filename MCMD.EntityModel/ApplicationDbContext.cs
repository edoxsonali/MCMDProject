
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MCMD.EntityModel
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("IdentityConnection")
        {
        }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Speciality> Specialitys { get; set; }

        public DbSet<UserLogin> UserLogins { get; set; }

        public DbSet<UserLoginRole> UserLoginRoles { get; set; }

        public DbSet<UserLoginSpeciality> UserLoginSpecialitys { get; set; }

        public DbSet<MCMDMembership> Memberships { get; set; }

        public DbSet<Duration> DurationList { get; set; }

        public DbSet<AutoRenaval> AutoRenavals { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<Help> Helps { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Map Entities to their tables.
            //modelBuilder.Entity<ApplicationUser>().ToTable("MCMDUsers");

            //modelBuilder.Entity<ApplicationUserClaim>().ToTable("MCMDUserClaims");
            //modelBuilder.Entity<ApplicationUserLogin>().ToTable("MCMDUserLogins");
            //modelBuilder.Entity<ApplicationUserRole>().ToTable("MCMDUserRoles");


            modelBuilder.Entity<Role>().ToTable("MCMDRoles");
            modelBuilder.Entity<Role>().HasKey<int>(r => r.RoleId);

            modelBuilder.Entity<Speciality>().ToTable("Speciality");
            modelBuilder.Entity<Speciality>().HasKey<int>(r => r.SpecialityID);

            modelBuilder.Entity<UserLogin>().ToTable("Login");
            modelBuilder.Entity<UserLogin>().HasKey<int>(r => r.LoginId);

            modelBuilder.Entity<UserLoginRole>().ToTable("Login_Role");
            modelBuilder.Entity<UserLoginRole>().HasKey<int>(r => r.LoginRoleId);

            modelBuilder.Entity<UserLoginSpeciality>().ToTable("Login_Speciality");
            modelBuilder.Entity<UserLoginSpeciality>().HasKey<int>(r => r.LoginSpecialityId);

            modelBuilder.Entity<MCMDMembership>().ToTable("Membership");
            modelBuilder.Entity<MCMDMembership>().HasKey<int>(r => r.MembershipId);

            modelBuilder.Entity<Duration>().ToTable("Duration");
            modelBuilder.Entity<Duration>().HasKey<int>(r => r.DurationId);

            modelBuilder.Entity<AutoRenaval>().ToTable("AutoRenaval");
            modelBuilder.Entity<AutoRenaval>().HasKey<int>(r => r.AutoRenavalId);

            modelBuilder.Entity<Help>().ToTable("Help");
            modelBuilder.Entity<Help>().HasKey<int>(r => r.HelpId);



            //  modelBuilder.Entity<Role>().ToTable("MCMDCreateRoles");
            // Set AutoIncrement-Properties
            //modelBuilder.Entity<ApplicationUser>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<ApplicationUserClaim>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<ApplicationRole>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }

    }
}
