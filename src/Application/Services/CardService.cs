using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Card.Command.CreateOrUpdateCardCommand;
using Application.Card.Queries.GetAllCardQuery;
using Application.Common.Services;
using Application.Interfaces;

namespace Application.Services
{
    public class CardService : ICardService
    {
        public Task CreateOrUpdateCardAsync(CreateOrUpdateCardCommand request)
        {
            //Handle logic
            throw new NotImplementedException();
        }

        public Task<List<GetAllCardResponseDto>> GetAllCardAsync()
        {
            //Handle logic
            throw new NotImplementedException();
        }

        public Task<SPayResponse<bool>> GetCardAsync(int id)
        {
            //Handle logic
            throw new NotImplementedException();
        }
    }
}
