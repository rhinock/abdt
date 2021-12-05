using System.Linq;
using System.Threading.Tasks;
using _02.Commands;
using _02.Entities;
using _02.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _02.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IRepository repository;
        private readonly IMediator mediator;

        public CardsController(
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
            => await repository.Create(card);

        [HttpPost("existed/{id:long}")]
        public async Task<ActionResult> UpdateWithExisted([FromBody] Card card, [FromRoute] long id)
        {
            card.Id = id;

            await mediator.Send(new UpdateCardCommand
            {
                Card = card
            });

            return Ok();
        }

        [HttpPost("{id:long}")]
        public async Task<ActionResult> Update([FromBody] Card card, [FromRoute] long id)
        {
            card.Id = id;
            context.Update(card);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            var existed = await context.FindAsync<Card>(id);

            if (existed == null)
                return BadRequest();

            var entry = context.Entry(existed);
            entry.State = EntityState.Deleted;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("tracking/{id:long}")]
        public async Task<ActionResult<Card>> GetSimple([FromRoute] long id)
        {
            var card = await context.FindAsync<Card>(id);
            var entry = context.Entry(card);
            return Ok(card);
        }

        [HttpGet("no-tracking/{id:long}")]
        public async Task<ActionResult<Card>> GetWithUpdate([FromRoute] long id)
        {
            var card = await context.Cards
                .Where(x => x.Id == id)
                .AsTracking()
                .FirstAsync();

            return Ok(card);
        }
    }
}