using Microsoft.EntityFrameworkCore;
using SPay.BO.ReferenceSRC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.DAO.ReferenceSRC
{
    public class RoleDAO
    {
        private readonly ITCenterContext _dbContext = null;

        private static RoleDAO instance = null;
        public static RoleDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoleDAO();
                }
                return instance;
            }
        }

        public RoleDAO()
        {
            if (_dbContext == null)
            {
                _dbContext = new ITCenterContext();
            }
        }

        public async void CreateRole(Role newRole)
        {
            Role role = _dbContext.Roles.FirstOrDefault(x => x.RoleId.Equals(newRole.RoleId));

            if (role == null)
            {
                _dbContext.Roles.Add(newRole);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _dbContext.Roles.ToListAsync();
        }
    }
}
