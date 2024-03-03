using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SPay.BO.DTOs.Admin.Card.Response;
using SPay.Repository;

namespace SPay.Service
{
    public interface ICardService
    {
        Task<SPayResponse<IList<GetAllCardResponse>>> GetAllCardsAsync();
    }
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepo;
        private readonly IMapper _mapper;
        public CardService(ICardRepository _cardRepo, IMapper mapper)
        {
            this._cardRepo = _cardRepo;
            this._mapper = mapper;
        }
        public async Task<SPayResponse<IList<GetAllCardResponse>>> GetAllCardsAsync()
        {
            var result = new SPayResponse<IList<GetAllCardResponse>>();
            try
            {
                var cards = await _cardRepo.GetAllAsync();
                var cardsRes = _mapper.Map<IList<GetAllCardResponse>>(cards);
                result.Data = cardsRes;
                result.Success = true;
                result.Message = "Card retrieved successfully";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error in : CardService funciton GetAllCardsAsync()";
                result.ErrorMessages = new List<string> { ex.Message };
            }
            return result;
        }
    }
}
