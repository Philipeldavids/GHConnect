using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetAllAsync();

        Task<DepartmentDetailDto> GetByIdAsync(Guid id);

        Task CreateAsync(CreateDepartmentDto dto);

        Task UpdateAsync(Guid id, UpdateDepartmentDto dto);

        Task DeleteAsync(Guid id);

        Task AssignMemberAsync(
            AssignMemberDepartmentDto dto);

        Task RemoveMemberAsync(
            Guid memberId,
            Guid departmentId);
    }
}
