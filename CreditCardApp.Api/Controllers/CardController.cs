using CreditCardApp.Application.Queries.Cards;
using CreditCardApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using CreditCardApp.Application.Commands.Cards;
using MediatR;

namespace CreditCardApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly PdfGeneratorService _pdfGeneratorService;
        private readonly IMediator _mediator;

        public CardController(IMediator mediator, PdfGeneratorService pdfGeneratorService)
        {
            _mediator = mediator;
            _pdfGeneratorService = pdfGeneratorService;
        }

        // GET: api/card
        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            var cards = await _mediator.Send(new GetCardsQuery());
            return Ok(cards);
        }

        // GET: api/card/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCard(int id)
        {
            var card = await _mediator.Send(new GetCardByIdQuery(id));
            if (card == null) return NotFound("Tarjeta no encontrada.");
            return Ok(card);
        }

        // POST: api/card
        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cardId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCard), new { id = cardId }, command);
        }

        // PUT: api/card/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(int id, [FromBody] UpdateCardCommand command)
        {
            if (id != command.Id) return BadRequest("ID en la URL no coincide con el ID del comando.");

            var result = await _mediator.Send(command);
            if (!result) return NotFound("Tarjeta no encontrada.");

            return NoContent();
        }


        // DELETE: api/card/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var result = await _mediator.Send(new DeleteCardCommand { Id = id });
            if (!result) return NotFound();

            return NoContent();
        }


        // GET: api/card/{id}/statement
        [HttpGet("{id}/statement")]
        public async Task<IActionResult> GetCardStatement(int id)
        {
            var statement = await _mediator.Send(new GetCardStatementQuery(id));
            if (statement == null) return NotFound("Tarjeta no encontrada.");
            return Ok(statement);
        }

        [HttpGet("{id}/export-pdf")]
        public async Task<IActionResult> ExportToPdf(int id)
        {
            var card = await _mediator.Send(new GetCardByIdQuery(id));
            if (card == null) return NotFound("Tarjeta no encontrada.");

            var pdfBytes = _pdfGeneratorService.GenerateStatementPdf(card);
            return File(pdfBytes, "application/pdf", "EstadoDeCuenta.pdf");
        }
    }
}
