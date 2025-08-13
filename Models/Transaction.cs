using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensePlanner.Api.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public int CategoryId { get; set; } // Foreign key to Category
        public int AccountId { get; set; } // Foreign key to Account
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string Description { get; set; } = string.Empty;

        public User User { get; set; } = null!; // Navigation property to User
        public Category Category { get; set; } = null!; // Navigation property to Category
        public Account Account { get; set; } = null!; // Navigation property to Account
    }
}