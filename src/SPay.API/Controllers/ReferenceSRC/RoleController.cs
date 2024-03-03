using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPay.API.Controllers.ReferenceSRC.Constants;
using SPay.BO.ReferenceSRC.Models;
using SPay.Service.ReferenceSRC;

namespace SPay.API.Controllers.RerferenceSRC
{
    //[ApiController]
    //[Authorize]
    //public class RoleController : ControllerBase
    //{
    //    private readonly IRoleService _roleService;

    //    public RoleController(IRoleService roleService)
    //    {
    //        _roleService = roleService;
    //    }

    //    [HttpGet(ApiEndPointConstant.Role.RolesEndPoint)]
    //    public async Task<List<Role>> GetAllRoles()
    //    {
    //        return await _roleService.GetAllRoles();
    //    }

    //    [HttpPost(ApiEndPointConstant.Role.RolesEndPoint)]
    //    public async Task<IActionResult> CreateRole(Role role)
    //    {
    //        _roleService.CreateRole(role);
    //        return Ok(role);
    //    }
    //}
}
