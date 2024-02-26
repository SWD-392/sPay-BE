using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Card.Queries.GetAllCardQuery
{
    public class GetAllCardQuery : IRequest<List<GetAllCardResponseDto>>
    {
    }
}
