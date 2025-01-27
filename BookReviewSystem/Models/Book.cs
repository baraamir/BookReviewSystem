using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookReviewSystem.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Range(1900, 2024)]
        public int PublishedYear { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }

        [ForeignKey("Author")]
        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}