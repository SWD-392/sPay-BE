using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Card.Command.CreateOrUpdateCardCommand;
using Application.Card.Queries.GetAllCardQuery;
using Application.Common.Services;

namespace Application.Interfaces
{
    public interface ICardService
    {
        Task<SPayResponse<bool>> GetCardAsync(int id);
        Task<List<GetAllCardResponseDto>> GetAllCardAsync();
        Task CreateOrUpdateCardAsync(CreateOrUpdateCardCommand request);
    }
}
