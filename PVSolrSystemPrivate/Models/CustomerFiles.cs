using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PVSolrSystemPrivate.Models
{
    public class CustomerFile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? CustomerId { get; set; }

        [Required]
        [StringLength(255)]
        public string? FileName { get; set; }

        [Required]
        [StringLength(255)]
        public string? FilePath { get; set; }

        // Navigation property to Customer
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
    }
}