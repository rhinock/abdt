using _01.Commands;
using _01.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IRepository repository;
        private readonly IMediator mediator;

        public CardController(
            AppDbContext context,
            IRepository repository,
            IMediator mediator)
        {
            this.context = context;
            this.repository = repository;
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create([FromBody] Card card)
        {
            await repository.Create(card);
            return card.Id;
        }

        [HttpPost("existed/{id}")]
        public async Task<ActionResult> UpdateExisted([FromBody] Card card, [FromRoute] long id)
        {
            card.Id = id;

            await mediator.Send(new UpdateCardCommand
            {
                Card = card
            });

            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> Update([FromBody] Card card, [FromRoute] long id)
        {
            card.Id = id;

            context.Update(card);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            var current = await context.FindAsync<Card>(id);

            if (current is null)
                return BadRequest();

            var entry = context.Entry(current);
            entry.State = EntityState.Deleted;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> Get([FromRoute] long id)
        {
            var card = await context.Cards
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return card;
        }
    }
}
