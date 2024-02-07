using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{

    public class CareerCloudContext : DbContext
    {
        public CareerCloudContext(DbContextOptions<CareerCloudContext> options) : base(options)
        {

        }

        public DbSet<ApplicantEducationPoco> ApplicantEducation { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfile { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResume { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkill { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        //public DbSet<ApplicationEducationPoco> ApplicationEducation { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescription { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescription { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducation { get; set; }
        public DbSet<CompanyJobPoco> CompanyJob { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkill { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocation { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfile { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogin { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRole { get; set; }
        public DbSet<SecurityRolePoco> SecurityRole { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCode { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCode { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Use a connection string
                //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-FBQDQG2\SQLEXPRESS;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False");
                optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-8K1LH9HV;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define foreign key relationships here, if needed
            // Example:
            // modelBuilder.Entity<CompanyLocationPoco>()
            //     .HasOne(e => e.Company) // Assuming a navigation property named 'Company'
            //     .WithMany()
            //     .HasForeignKey(e => e.CompanyId); // Assuming a foreign key property named 'CompanyId'

            modelBuilder.Entity<SystemCountryCodePoco>()
              .HasKey(e => e.Code);

            modelBuilder.Entity<SystemLanguageCodePoco>()
               .HasKey(e => e.LanguageID);

            modelBuilder.Entity<ApplicantEducationPoco>()
               .HasOne(a => a.ApplicantProfile)
               .WithMany(b => b.ApplicantEducations)
               .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
               .HasOne(a => a.ApplicantProfile)
               .WithMany(b => b.ApplicantJobApplications)
               .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
               .HasOne(a => a.CompanyJob)
               .WithMany()
               .HasForeignKey(b => b.Job);

            modelBuilder.Entity<ApplicantProfilePoco>()
               .HasOne(a => a.SystemCountryCode)
               .WithMany()
               .HasForeignKey(b => b.Country);

            modelBuilder.Entity<CompanyLocationPoco>()
               .HasOne(a => a.SystemCountryCode)
               .WithMany()
               .HasForeignKey(b => b.CountryCode);


            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
               .HasOne(a => a.SystemCountryCode)
               .WithMany()
               .HasForeignKey(b => b.CountryCode);

            modelBuilder.Entity<ApplicantProfilePoco>()
               .HasMany(a => a.ApplicantResumes)
               .WithOne(b => b.ApplicantProfile)
               .HasForeignKey(b => b.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
               .HasMany(a => a.ApplicantSkills)
               .WithOne(b => b.ApplicantProfile)
               .HasForeignKey(b => b.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
               .HasMany(a => a.ApplicantWorkHistorys)
               .WithOne(b => b.ApplicantProfile)
               .HasForeignKey(b => b.Applicant);

           

            modelBuilder.Entity<SystemLanguageCodePoco>()
               .HasMany(a => a.CompanyDescription)
               .WithOne(b => b.SystemLanguageCode)
               .HasForeignKey(b => b.LanguageId);

            /*   modelBuilder.Entity<SystemLanguageCodePoco>()
                  .HasKey(e => e.LanguageID);*/

            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .HasOne(e => e.SecurityLogin)
                .WithMany(f => f.SecurityLoginsLogs)
                .HasForeignKey(e => e.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(e => e.SecurityRole)
                .WithMany(f => f.SecurityLoginsRoles)
                .HasForeignKey(e => e.Role);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(e => e.SecurityLoginsRoles)
                .WithOne(f => f.SecurityLogin)
                .HasForeignKey(e => e.Login);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(e => e.SecurityLoginsLogs)
                .WithOne(f => f.SecurityLogin)
                .HasForeignKey(e => e.Login);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(e => e.ApplicantProfiles)
                .WithOne(f => f.SecurityLogin)
                .HasForeignKey(e => e.Login);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(cd => cd.CompanyProfile)
                .WithMany(cp => cp.CompanyLocations)
                .HasForeignKey(cd => cd.Company);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(cd => cd.CompanyProfile)
                .WithMany(cp => cp.CompanyDescriptions)
                .HasForeignKey(cd => cd.Company);

           /* modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(e => e.SystemLanguageCode);
*/
            modelBuilder.Entity<CompanyJobPoco>()
               .HasOne(cd => cd.CompanyProfile)
                .WithMany(cp => cp.CompanyJobs)
                .HasForeignKey(cd => cd.Id);

            modelBuilder.Entity<CompanyJobPoco>()
               .HasMany(a => a.CompanyJobDescriptions)
                .WithOne(b => b.CompanyJob)
                .HasForeignKey(b => b.Job);

            modelBuilder.Entity<CompanyJobPoco>()
               .HasMany(a => a.ApplicantJobApplication)
                .WithOne(b => b.CompanyJob)
                .HasForeignKey(b => b.Job);

            modelBuilder.Entity<CompanyJobPoco>()
               .HasMany(a => a.CompanyJobEducations)
                .WithOne(b => b.CompanyJob)
                .HasForeignKey(b => b.Job);

            modelBuilder.Entity<CompanyJobPoco>()
               .HasMany(a => a.CompanyJobSkills)
                .WithOne(b => b.CompanyJob)
                .HasForeignKey(b => b.Job);


            modelBuilder.Entity<ApplicantEducationPoco>()
                .HasOne(cd => cd.ApplicantProfile)
                .WithMany(cp => cp.ApplicantEducations)
                .HasForeignKey(cd => cd.Applicant);
            

            // Repeat for other POCOs as required

            base.OnModelCreating(modelBuilder);
        }
    }
}
