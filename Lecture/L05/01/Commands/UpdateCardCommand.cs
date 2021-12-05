using _01.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace _01.Commands
{
    public class UpdateCardCommand : IRequest
    {
        public Card Card { get; set; }

        public class UpdateCardCommandHandler : IRequestHandler<UpdateCardCommand>
        {
            private readonly IRepository repository;

            public UpdateCardCommandHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
            {
                await repository.Update(request.Card);
                return Unit.Value;
            }
        }
    }
}
