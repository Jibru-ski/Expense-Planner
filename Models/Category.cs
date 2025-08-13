using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensePlanner.Api.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public int Name { get; set; }
        public CategoryType Type { get; set; }

        public User User { get; set; } = null!; // Navigation property to User
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); // Navigation property for transactions

    }

    public enum CategoryType
    {
        Income,
        Expense
    }

}
