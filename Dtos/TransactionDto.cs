using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace celsia_assetsment_johan_arboleda.Dtos
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public DateTime TransactionTime { get; set; }
        public string PlatformName { get; set; }
        public string TransactionStatus { get; set; }
    }
}