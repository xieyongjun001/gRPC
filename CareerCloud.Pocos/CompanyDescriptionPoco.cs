using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Descriptions")]
    public class CompanyDescriptionPoco : IPoco
    {
        /*[Key]*/
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Company_Name")]
        public string CompanyName { get; set; }

        [Column("Company_Description")]
        public string CompanyDescription { get; set; }

        [Column("Time_Stamp")]
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        [Required]
        [Column("Company")]
        public Guid Company { get; set; }

        [Required]
        [Column("LanguageID")]
        public string LanguageId { get; set; }

        public virtual CompanyProfilePoco CompanyProfile { get; set; }
        /*public virtual CompanyJobPoco CompanyJob { get; set; }*/
        public virtual SystemLanguageCodePoco SystemLanguageCode { get; set; }
        /*public virtual ICollection<CompanyJobPoco> CompanyJobs { get; set; }*/
    }
}
