using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Role.Response
{
	public class RoleResponse
	{
		public int No { get; set; } = 0;
		public string RoleKey { get; set; } = null!;
		public string? RoleName { get; set; }
		public string? Description { get; set; }
	}
}
