using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    public interface IPoco
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
    }
}