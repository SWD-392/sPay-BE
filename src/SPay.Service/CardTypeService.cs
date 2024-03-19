using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPay.BO.DTOs.CardType.Request;
using SPay.BO.DTOs.CardType.Response;
using SPay.BO.Extention.Paginate;
using SPay.Service.Response;

namespace SPay.Service
{
	public interface ICardTypeService
	{
		Task<SPayResponse<PaginatedList<CardTypeResponse>>> GetListCardTypeAsync(GetListCardTypeRequest request);
		Task<SPayResponse<CardTypeResponse>> GetCardTypeByKeyAsync(string key);
		Task<SPayResponse<bool>> DeleteCardTypeAsync(string key);
		Task<SPayResponse<bool>> CreateCardTypeAsync(CreateOrUpdateCardTypeRequest request);
		Task<SPayResponse<bool>> UpdateCardTypeAsync(string key, CreateOrUpdateCardTypeRequest request);
	}

	public class CardTypeService
	{
	}
}
