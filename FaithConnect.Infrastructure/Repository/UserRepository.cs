using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using FaithConnect.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetRoles(Guid OrgId)
        {
            return await _context.Roles.Where(x => x.ChurchId == OrgId).ToListAsync();
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetByIdAsync(string id, Guid OrgId) => await _context.Users.Where(x => x.Id == id && x.ChurchId == OrgId).FirstOrDefaultAsync();
        public async Task<User> AddAsync(User user) { _context.Users.Add(user); await _context.SaveChangesAsync(); return user; }
        public async Task UpdateAsync(User user) { _context.Users.Update(user); await _context.SaveChangesAsync(); }

        public async Task<List<User>> GetAllByOrganizationAsync(Guid orgId)
        {
            return await _context.Users
                .Where(u => u.ChurchId == orgId)
                .ToListAsync();
        }

        //public async Task<List<Permission>> GetPermission()
        //{
        //    return await _ctx.Permissions.ToListAsync();
        //}

        public async Task<bool> CreateRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

