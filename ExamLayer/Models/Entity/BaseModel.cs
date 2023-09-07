using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamLayer.Models.Entity
{
    public class BaseModel
    {
        [Key]
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("ADDED_DATE")]
        public DateTime AddedDate { get; set; }

        [Required]
        [Column("ADDED_USER")]
        public Guid AddedUser { get; set; }

        [Column("MODIFIED_DATE")]
        public DateTime? ModifiedDate { get; set; }

        [Column("MODIFIED_USER")]
        public Guid? ModifiedUser { get; set; }

        [Required]
        [Column("IS_DELETED")]
        public bool IsDeleted { get; set; } = false;

        [Column("DELETED_DATE")]
        public DateTime? DeletedDate { get; set; }

        [Column("DELETED_USER")]
        public Guid? DeletedUser { get; set; }
    }
}
