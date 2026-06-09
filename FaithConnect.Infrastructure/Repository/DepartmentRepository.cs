using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using FaithConnect.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Infrastructure.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context) {
        
            _context = context;
        }

        public async Task<List<Department>> GetAllAsync(Guid OrgId)
        {
            return await _context.Departments
                .Include(x => x.MemberDepartments)
                .Where(x =>
                    x.ChurchId == OrgId)
                .ToListAsync();
        }
        public async Task<Department?> GetByIdAsync(Guid OrgId, Guid id)
        {
            return await _context.Departments
                .Include(x => x.MemberDepartments)
                    .ThenInclude(x => x.Member)
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.ChurchId == OrgId);
        }

        public async Task CreateAsync(
    Department department)
        {
            await _context.Departments
                .AddAsync(department);

            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(
    string name, Guid OrgId,
    Guid? excludeId = null)
        {
            return await _context.Departments
                .AnyAsync(x =>
                    x.ChurchId == OrgId &&
                    x.Name.ToLower() == name.ToLower() &&
                    (!excludeId.HasValue || x.Id != excludeId));
        }
        public async Task RemoveMemberAsync(
    Guid memberId,
    Guid departmentId)
        {
            var record =
                await _context.MemberDepartments
                    .FirstOrDefaultAsync(x =>
                        x.MemberId == memberId &&
                        x.DepartmentId == departmentId);

            if (record == null)
                return;

            _context.MemberDepartments.Remove(record);

            await _context.SaveChangesAsync();
        }
        public async Task<bool> MemberExistsAsync(
    Guid memberId,
    Guid departmentId)
        {
            return await _context.MemberDepartments
                .AnyAsync(x =>
                    x.MemberId == memberId &&
                    x.DepartmentId == departmentId);
        }
        public async Task UpdateAsync(
    Department department)
        {
            _context.Departments.Update(
                department);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Department department)
        {
            _context.Departments.Remove(
                department);

            await _context.SaveChangesAsync();
        }
        public async Task AssignMemberAsync(
    MemberDepartment memberDepartment)
        {
            await _context.MemberDepartments
                .AddAsync(memberDepartment);

            await _context.SaveChangesAsync();
        }
    }
}
