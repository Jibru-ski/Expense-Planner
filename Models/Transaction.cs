using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensePlanner.Api.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public int AccountId { get; set; } // Foreign key to Account
        public TransactionType Type { get; set; } // Enum for transaction type (Income/Expense)

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string Description { get; set; } = string.Empty;

        public User User { get; set; } = null!; // Navigation property to User
        public Account Account { get; set; } = null!; // Navigation property to Account
    }

    public enum TransactionType
    {
        Income,
        Expense
    }
}