using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Infrastructure.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync(Guid OrgId);

        Task<Department?> GetByIdAsync(Guid OrgId, Guid id);

        Task CreateAsync(Department department);

        Task UpdateAsync(Department department);

        Task DeleteAsync(Department department);

        Task AssignMemberAsync(MemberDepartment memberDepartment);

        Task RemoveMemberAsync(Guid memberId, Guid departmentId);
        Task<bool> ExistsAsync(
    string name, Guid OrgId,
    Guid? excludeId = null);
        Task<bool> MemberExistsAsync(
            Guid memberId,
            Guid departmentId);
    }
}
