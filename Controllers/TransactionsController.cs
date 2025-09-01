using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpensePlanner.Api.Data;
using ExpensePlanner.Api.Models;
using System.Transactions;
using ExpensePlanner.Api.Dtos.Transaction;

namespace ExpensePlanner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransactions([FromQuery] int? userId, TransactionDto dto)
        {
            var query = _context.Transactions.AsQueryable();

            if (userId != null)
            {
                query.Where(a => a.UserId == userId);
            }

            var transactions = await query
            .Select(t => new TransactionDto
            {
                TransactionId = t.TransactionId,
                Description = t.Description,
                Type = t.Type,
                Amount = t.Amount,
                CreatedOn = t.CreatedOn
            }).ToListAsync();

            return Ok();
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, UpdateTransactionDto dto)
        {
            var transaction =  await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            transaction.ModifiedOn = DateTime.UtcNow;
            transaction.Description = dto.Description;
            transaction.Amount = dto.Amount;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransactionDto>> CreateTransaction(CreateTransactionDto dto)
        {
            var transaction = new Models.Transaction
            {
                Description = dto.Description,
                Type = dto.Type,
                Amount = dto.Amount,
                CreatedOn = dto.CreatedOn                
            };  

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            var result = new TransactionDto
            {
                TransactionId = transaction.TransactionId,
                Description = transaction.Description,
                Type = transaction.Type,
                Amount = transaction.Amount,
                CreatedOn = transaction.CreatedOn
            };

            return CreatedAtAction(nameof(GetTransactions), new { id = transaction.TransactionId }, transaction);
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
