using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensePlanner.Api.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsActive  { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalExpense { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalIncome { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        public User User { get; set; } = null!; // Navigation property to User
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); // Navigation property for transactions

    }
}