using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace celsia_assetsment_johan_arboleda.Models
{
    public class Transaction
    {
        [Key]
        public int IdTransaction { get; set; }

        [Required]
        [StringLength(45)]
        public string PaymentReference { get; set; }

        [ForeignKey("DocumentType")]
        public int PayerTypeDocumentId { get; set; }
        public DocumentType DocumentType { get; set; }

        [Required]
        [StringLength(45)]
        public string PayerIdentification { get; set; }

        [Required]
        [StringLength(45)]
        public string PayerName { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TransactionAmount { get; set; }

        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }

        [ForeignKey("Platform")]
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}