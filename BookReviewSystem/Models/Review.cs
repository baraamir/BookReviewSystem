using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookReviewSystem.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [ForeignKey("Book")]
        public int BookID { get; set; }
        public virtual Book Book { get; set; }

        [Required]
        [StringLength(50)]
        public string ReviewerName { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}