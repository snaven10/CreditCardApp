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
    public class PurchaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PurchaseController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/purchase
        [HttpGet]
        public async Task<IActionResult> GetPurchases()
        {
            var purchases = await _context.Purchases.ToListAsync();
            var purchaseDtos = _mapper.Map<List<PurchaseDto>>(purchases);
            return Ok(purchaseDtos);
        }

        // POST: api/purchase
        [HttpPost]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseDto purchaseDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var purchase = _mapper.Map<Purchase>(purchaseDto);
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPurchases), new { id = purchase.Id }, purchaseDto);
        }

        // PUT: api/purchase/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchase(int id, [FromBody] PurchaseDto purchaseDto)
        {
            if (id != purchaseDto.Id) return BadRequest("Purchase ID mismatch.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingPurchase = await _context.Purchases.FindAsync(id);
            if (existingPurchase == null) return NotFound();

            _mapper.Map(purchaseDto, existingPurchase);
            _context.Entry(existingPurchase).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/purchase/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null) return NotFound();

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
