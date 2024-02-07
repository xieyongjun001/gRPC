using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("System_Country_Codes")]
    public class SystemCountryCodePoco
    {
       /* [Key]
        [Column("Id")]
        public Guid Id { get; set; }*/

        /*[Key]*/
        [Column("Code")]
        public string Code { get; set; } = null!;

        [Column("Name")]
        public string Name { get; set; } = null!;

    }
}
