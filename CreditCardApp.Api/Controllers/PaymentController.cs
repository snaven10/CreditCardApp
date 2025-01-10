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
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PaymentController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/payment
        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _context.Payments.ToListAsync();
            var paymentDtos = _mapper.Map<List<PaymentDto>>(payments);
            return Ok(paymentDtos);
        }

        // POST: api/payment
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDto paymentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var payment = _mapper.Map<Payment>(paymentDto);
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPayments), new { id = payment.Id }, paymentDto);
        }

        // PUT: api/payment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentDto paymentDto)
        {
            if (id != paymentDto.Id) return BadRequest("Payment ID mismatch.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingPayment = await _context.Payments.FindAsync(id);
            if (existingPayment == null) return NotFound();

            _mapper.Map(paymentDto, existingPayment);
            _context.Entry(existingPayment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/payment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return NotFound();

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
