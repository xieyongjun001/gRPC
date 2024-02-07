﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Skills")]
    public class ApplicantSkillPoco : IPoco
    {
        /*[Key]*/
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Skill_Level")]
        public string SkillLevel { get; set; }

        [Column("Start_Month")]
        public byte StartMonth { get; set; }


        [Column("Start_Year")]
        public int StartYear { get; set; }


        [Column("End_Month")]
        public byte EndMonth { get; set; }

        [Column("End_Year")]
        public int EndYear { get; set; }


         [Column("Time_Stamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        [Required]
        [Column("Applicant")]
        public Guid Applicant { get; set; }

        [Required]
        [Column("Skill")]
        public string Skill{ get; set; }

        public virtual ApplicantProfilePoco ApplicantProfile { get; set; }
    }
}
