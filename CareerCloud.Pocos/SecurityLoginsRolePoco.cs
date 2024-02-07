using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins_Roles")]
    public class SecurityLoginsRolePoco : IPoco
    {
        /*[Key]*/
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Login")]
        public Guid Login { get; set; }

        [Column("Role")]
        public Guid Role { get; set; }

        [Column("Time_Stamp")]
        [Timestamp]
        public byte[]? TimeStamp { get; set; }
        public virtual SecurityLoginPoco SecurityLogin { get; set; }

        public virtual SecurityRolePoco SecurityRole { get; set; }
    }
}
