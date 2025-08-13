using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensePlanner.Api.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }

        public User User { get; set; } = null!; // Navigation property to User
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); // Navigation property for transactions

    }
}