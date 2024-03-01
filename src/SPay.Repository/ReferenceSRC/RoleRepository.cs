using SPay.BO.ReferenceSRC.Models;
using SPay.DAO.ReferenceSRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Repository.ReferenceSRC
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetAllRoles();

        public void CreateRole(Role newRole);
        public void CreateRole1(Role newRole);

    }

    public class RoleRepository : IRoleRepository
    {
        public async Task<List<Role>> GetAllRoles() => await RoleDAO.Instance.GetAllRoles();

        public async void CreateRole(Role newRole) => RoleDAO.Instance.CreateRole(newRole);

        public void CreateRole1(Role newRole)
        {
            throw new NotImplementedException();
        }
    }
}
