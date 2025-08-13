using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensePlanner.Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Account> Accounts { get; set; } = new List<Account>(); // Navigation property for accounts
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); // Navigation property for transactions
        public ICollection<Category> Categories { get; set; } = new List<Category>(); // Navigation property for categories

    }
}