using FaithConnect.Application.Interfaces;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.Interfaces;
using FaithConnect.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ITenantService _tenantService;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMemberRepository _memberRepository;
        public DepartmentService(ITenantService tenantService, IMemberRepository memberRepository, IDepartmentRepository departmentRepository)
        {
            _tenantService = tenantService;
            _memberRepository = memberRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task CreateAsync(
    CreateDepartmentDto dto)
        {
            var exists =
                await _departmentRepository
                    .ExistsAsync(dto.Name, _tenantService.GetTenantId());

            if (exists)
                throw new Exception(
                    "Department already exists");

            var department = new Department
            {
                Name = dto.Name,
                Description = dto.Description,
                ChurchId = _tenantService.GetTenantId(),
            };

            await _departmentRepository
                .CreateAsync(department);
        }
        public async Task AssignMemberAsync(
    AssignMemberDepartmentDto dto)
        {
            var orgId = _tenantService.GetTenantId();
            var department =
                await _departmentRepository
                    .GetByIdAsync(orgId,dto.DepartmentId);

            if (department == null)
                throw new Exception(
                    "Department not found");

            var member =
                await _memberRepository
                    .GetByIdAsync(dto.MemberId, orgId);

            if (member == null)
                throw new Exception(
                    "Member not found");

            var exists =
                await _departmentRepository
                    .MemberExistsAsync(
                        dto.MemberId,
                        dto.DepartmentId);

            if (exists)
                throw new Exception(
                    "Member already assigned to this department");

            await _departmentRepository
                .AssignMemberAsync(
                    new MemberDepartment
                    {
                        MemberId = dto.MemberId,
                        DepartmentId = dto.DepartmentId
                    });
        }
        public async Task<List<DepartmentDto>> GetAllAsync()
        {

            var orgID = _tenantService.GetTenantId();
            var departmnts =
                await _departmentRepository.GetAllAsync(orgID);

            var departments = new List<DepartmentDto>();
           
            foreach (var dep in departmnts)
            {
                DepartmentDto departmentDto = new DepartmentDto();
                departmentDto.Id = dep.Id;
                departmentDto.Name = dep.Name;
                departmentDto.Description = dep.Description;
                departmentDto.MemberCount = dep.MemberDepartments.Count();

                departments.Add(departmentDto);
            }
            
            return departments;

        }
        public async Task<DepartmentDetailDto> GetByIdAsync(Guid id)
        {
            var orgID = _tenantService.GetTenantId();
            var department =
                await _departmentRepository.GetByIdAsync(orgID, id);

            if (department == null)
                throw new Exception("Department not found");

            return new DepartmentDetailDto
            {
                Id = department.Id,

                Name = department.Name,

                Description =
                    department.Description,

                Members = department
                    .MemberDepartments
                    .Select(x =>
                        new DepartmentMemberDto
                        {
                            MemberId =
                                x.Member.Id,

                            MembershipNumber =
                                x.Member.MembershipNumber,

                            FullName =
                                $"{x.Member.FirstName} {x.Member.LastName}",

                            Email =
                                x.Member.Email,

                            PhoneNumber =
                                x.Member.PhoneNumber
                        })
                    .ToList()
            };
        }
        public async Task UpdateAsync(
     Guid id,
     UpdateDepartmentDto dto)
        {
            var OrgID = _tenantService.GetTenantId();
            var department =
                await _departmentRepository
                    .GetByIdAsync(OrgID, id);

            if (department == null)
                throw new Exception(
                    "Department not found");

            var exists =
                await _departmentRepository
                    .ExistsAsync(dto.Name, id);

            if (exists)
                throw new Exception(
                    "Department already exists");

            department.Name =
                dto.Name;

            department.Description =
                dto.Description;

            await _departmentRepository
                .UpdateAsync(department);
        }


        public async Task DeleteAsync(Guid id)
        {

            var orgId = _tenantService.GetTenantId();
            var department =
                await _departmentRepository
                    .GetByIdAsync(orgId, id);

            if (department == null)
                throw new Exception(
                    "Department not found");

            await _departmentRepository
                .DeleteAsync(department);
        }
        public async Task RemoveMemberAsync(
    Guid memberId,
    Guid departmentId)
        {
            await _departmentRepository
                .RemoveMemberAsync(
                    memberId,
                    departmentId);
        }
    }
}