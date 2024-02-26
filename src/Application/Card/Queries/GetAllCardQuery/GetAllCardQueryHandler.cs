using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Card.Queries.GetAllCardQuery
{
    public class GetAllCardQueryHandler : IRequestHandler<GetAllCardQuery, List<GetAllCardResponseDto>>
    {
        private readonly ICardService _cardService;
        public GetAllCardQueryHandler(ICardService _cardService)
        {
            this._cardService = _cardService;
        }
        public Task<List<GetAllCardResponseDto>> Handle(GetAllCardQuery request, CancellationToken cancellationToken)
        {
            return _cardService.GetAllCardAsync();
        }
    }
}
