using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensePlanner.Api.Dtos.Account
{
    public class AccountSummaryDto
    {
        public int AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal Balance { get; set; } 
        public DateTime CreatedOn { get; set; }

    }
}