using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace celsia_assetsment_johan_arboleda.Models
{
    public class Invoice
    {
        [Key]
        public int IdInvoice { get; set; }

        [Required]
        [StringLength(45)]
        public string InvoiceNumber { get; set; }

        [Required]
        public int BillingYear { get; set; }

        [Required]
        public int BillingMonth { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal BilledAmount { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}