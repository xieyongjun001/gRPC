﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Work_History")]
    public class ApplicantWorkHistoryPoco : IPoco
    {
        /*[Key]*/
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Company_Name")]
        public string CompanyName { get; set; }

        [Column("Country_Code")]
        public string CountryCode { get; set; }


        [Column("Job_Title")]
        public string JobTitle { get; set; }

        [Column("Job_Description")]
        public string JobDescription { get; set; }

        [Column("Start_Month")]
        public short StartMonth { get; set; }

        [Column("Start_Year")]
        public int StartYear { get; set; }

        [Column("End_Month")]
        public short EndMonth { get; set; }

        [Column("End_Year")]
        public int EndYear { get; set; }

        [Column("Time_Stamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        [Required]
        [Column("Applicant")]
        public Guid Applicant { get; set; }

        [Required]
        [Column("Location")]
        public String Location { get; set; }

        
        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }

        public virtual SystemCountryCodePoco SystemCountryCode { get; set; }

    }
}
