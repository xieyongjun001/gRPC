using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("System_Language_Codes")]
    public class SystemLanguageCodePoco
    {
       /* [Key]
        [Column("Id")]
        public Guid Id { get; set; }*/

        /* [Key]*/
        [Column("LanguageID")]
        public string LanguageID { get; set; } = null!;

        [Column("Name")]
        public string Name { get; set; } = null!;

        [Column("Native_Name")]
        public string NativeName { get; set; } = null!;

        public virtual ICollection<CompanyDescriptionPoco> CompanyDescription { get; set; }

    }
}
