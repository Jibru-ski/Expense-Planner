using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensePlanner.Api.Dtos.Transaction
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public ExpensePlanner.Api.Models.TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }


}