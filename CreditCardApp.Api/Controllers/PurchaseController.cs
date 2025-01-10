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

        // GET: api/purchase/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchase(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null) return NotFound();
            var purchaseDto = _mapper.Map<PurchaseDto>(purchase);
            return Ok(purchaseDto);
        }

        // POST: api/purchase
        [HttpPost]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseDto purchaseDto)
        {
            var purchase = _mapper.Map<Purchase>(purchaseDto);
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPurchase), new { id = purchase.Id }, purchaseDto);
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
