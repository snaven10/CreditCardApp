using AutoMapper;
using CreditCardApp.Api.Data;
using CreditCardApp.Api.DTOs;
using CreditCardApp.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreditCardApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CardController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/card
        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            var cards = await _context.Cards.ToListAsync();
            var cardDtos = _mapper.Map<List<CardDto>>(cards);
            return Ok(cardDtos);
        }

        // GET: api/card/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null) return NotFound();
            var cardDto = _mapper.Map<CardDto>(card);
            return Ok(cardDto);
        }

        // POST: api/card
        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] CardDto cardDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var card = _mapper.Map<Card>(cardDto);
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, cardDto);
        }

        // PUT: api/card/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(int id, [FromBody] CardDto cardDto)
        {
            if (id != cardDto.Id) return BadRequest("Card ID mismatch.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingCard = await _context.Cards.FindAsync(id);
            if (existingCard == null) return NotFound();

            _mapper.Map(cardDto, existingCard);
            _context.Entry(existingCard).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/card/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null) return NotFound();

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/card/{id}/statement
        [HttpGet("{id}/statement")]
        public async Task<IActionResult> GetCardStatement(int id)
        {
            var card = await _context.Cards.Include(c => c.Purchases).Include(c => c.Payments).FirstOrDefaultAsync(c => c.Id == id);
            if (card == null) return NotFound();

            var purchasesThisMonth = card.Purchases.Where(p => p.PurchaseDate.Month == DateTime.Now.Month).ToList();
            var totalPurchases = purchasesThisMonth.Sum(p => p.Amount);
            var totalPayments = card.Payments.Sum(p => p.Amount);

            var statement = new
            {
                CardHolderName = card.CardHolderName,
                CardNumber = card.CardNumber,
                CurrentBalance = card.CurrentBalance,
                CreditLimit = card.CreditLimit,
                AvailableCredit = card.CreditLimit - card.CurrentBalance,
                TotalPurchasesThisMonth = totalPurchases,
                TotalPayments = totalPayments,
                MinimumPaymentDue = card.CurrentBalance * 0.05m, // 5% del saldo como ejemplo
                FullPaymentWithInterest = card.CurrentBalance * 1.25m // 25% de interés como ejemplo
            };

            return Ok(statement);
        }
    }
}
