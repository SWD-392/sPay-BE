using Microsoft.EntityFrameworkCore;
using SPay.BO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.Extention.Paginate
{
    public static class PaginateExtenstion
    {
		private static readonly int FIRST_PAGE = 1;
		public static async Task<PaginatedList<T>> ToPaginateAsync<T>(this IEnumerable<T> enumerable, PagingRequest request)
		{
			if (request.PageIndex < FIRST_PAGE)
				throw new ArgumentException($"page ({request.PageIndex}) must be greater or equal to {FIRST_PAGE}");

			var total = await Task.Run(() => enumerable.Count());
			var items = enumerable.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToList();
			var totalPages = (int)Math.Ceiling(total / (double)request.PageSize);

			return new PaginatedList<T>
			{
				PageIndex = request.PageIndex,
				PageSize = request.PageSize,
				TotalCount = total,
				Items = items,
				TotalPages = totalPages
			};
		}

	}
}
