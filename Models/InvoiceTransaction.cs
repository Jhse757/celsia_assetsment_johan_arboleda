using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace celsia_assetsment_johan_arboleda.Models
{
    public class InvoiceTransaction
    {
        [Key]
        public int IdInvoiceTransaction { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}