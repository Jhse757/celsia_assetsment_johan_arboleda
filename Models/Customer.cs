using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace celsia_assetsment_johan_arboleda.Models
{
    public class Customer
    {
        [Key]
        public int IdCustomer { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        [Required]
        [StringLength(45)]
        public string LastName { get; set; }

        [ForeignKey("DocumentType")]
        public int TypeDocumentId { get; set; }
        public DocumentType DocumentType { get; set; }

        [Required]
        [StringLength(45)]
        public string NumberDocument { get; set; }

        [Required]
        [StringLength(125)]
        public string Email { get; set; }

        [ForeignKey("Prefix")]
        public int? TelephonePrefixId { get; set; }
        public Prefix Prefix { get; set; }

        [Required]
        [StringLength(45)]
        public string Phone { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}