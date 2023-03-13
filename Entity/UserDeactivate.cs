using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("user_deactivate")]
    public class UserDeactivate
    {
        [Key]
        [Column("user_deactivate_id")]
        public int UserDeactivateId { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("date_started")]
        public DateTime DateStarted { get; set; } = DateTime.Now;

        [Column("date_end")]
        public DateTime DateEnd { get; set; } = DateTime.Now.AddDays(5);
    }
}
