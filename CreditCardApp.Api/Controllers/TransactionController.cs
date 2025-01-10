using AutoMapper;
using CreditCardApp.Infrastructure.Persistence;
using CreditCardApp.Application.DTOs;
using CreditCardApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreditCardApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TransactionController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/transaction
        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _context.Transactions.ToListAsync();
            var transactionDtos = _mapper.Map<List<TransactionDto>>(transactions);
            return Ok(transactionDtos);
        }
    }
}
