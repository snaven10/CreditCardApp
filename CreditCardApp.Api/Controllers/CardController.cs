using AutoMapper;
using CreditCardApp.Api.Data;
using CreditCardApp.Api.Entities;
using CreditCardApp.Api.DTOs;
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

        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            var cards = await _context.Cards.ToListAsync();
            var cardDtos = _mapper.Map<List<CardDto>>(cards);
            return Ok(cardDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddCard(CardDto cardDto)
        {
            var card = _mapper.Map<Card>(cardDto);
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCards), new { id = card.Id }, cardDto);
        }
    }
}
