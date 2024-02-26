using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Card.Command.CreateOrUpdateCardCommand
{
    public class CreateOrUpdateCardCommandHandler : IRequestHandler<CreateOrUpdateCardCommand, Unit>
    {
        private readonly ICardService _cardService;
        public CreateOrUpdateCardCommandHandler(ICardService _cardService)
        {
            this._cardService = _cardService;
        }

        public async Task<Unit> Handle(CreateOrUpdateCardCommand request, CancellationToken cancellationToken)
        {
            //Xử lý logic ở đây
            await _cardService.CreateOrUpdateCardAsync(request);
            return Unit.Value;
        }
    }
}
