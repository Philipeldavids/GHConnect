using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using FaithConnect.Application.Interfaces;
using FaithConnect.Application.Utilities;
using FaithConnect.Domain.DTO;
using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using FaithConnect.Infrastructure.Interfaces;
using FaithConnect.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Services
{
   
        public class MemberService : IMemberService
        {
            private readonly IMemberRepository _repository;
            private readonly ITenantService _tenantService;
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
            public MemberService(
                IMemberRepository repository,IEmailService emailService, UserManager<User> userManager, IUserRepository userRepository,
                ITenantService tenantService, ApplicationDbContext context)
            {
                _repository = repository;
            _userManager = userManager;
                _context = context;
            _emailService = emailService;
                _tenantService = tenantService;
            _userRepository = userRepository;
            }

        public async Task<MemberDetailsDto> GetDetailsAsync(Guid id)
        {

            var churchId = _tenantService.GetTenantId();
            var member =
                await _repository.GetMemberDetailsAsync(churchId,id);

            if (member == null)
                throw new Exception("Member not found");

            return new MemberDetailsDto
            {
                Id = member.Id,

                MembershipNumber = member.MembershipNumber,

                FirstName = member.FirstName,

                LastName = member.LastName,

                Email = member.Email,

                PhoneNumber = member.PhoneNumber,

                Gender = member.Gender,

                MembershipDate = member.MembershipDate,

                IsActive = member.IsActive,

                Departments = member.MemberDepartments
                    .Select(x => x.Department.Name)
                    .ToList()
            };
        }
        public async Task<List<MemberCommunicationDto>> GetCommunicationHistoryAsync(Guid memberId)
        {
            var communications = await _repository
                .GetCommunicationHistoryAsync(memberId);

            return communications
                .Select(c => new MemberCommunicationDto
                {
                    Id = c.Id,
                    Channel = c.Channel,
                    Subject = c.Subject,
                    Message = c.Message,
                    Status = c.Status,
                    SentAt = c.SentAt
                })
                .ToList();
        }
        public async Task<List<MemberAttendanceDto>> GetAttendanceHistoryAsync(Guid memberId)
        {
            var attendances = await _repository
                .GetAttendanceHistoryAsync(memberId);

            return attendances
                .Select(a => new MemberAttendanceDto
                {
                    AttendanceId = a.Id,
                    ServiceName = a.Service.Name,
                    ServiceDate = a.Service.ServiceDate,
                    CheckInTime = a.CheckInTime,
                    Status = a.Status.ToString()
                })
                .ToList();
        }
        public async Task<BulkMemberUploadResponseDto>
BulkUploadAsync(IFormFile file)
        {
            var result =
                new BulkMemberUploadResponseDto();
            var orgId = _tenantService.GetTenantId();

            using var stream =
                new MemoryStream();

            await file.CopyToAsync(stream);

            using var workbook =
                new XLWorkbook(stream);

            var worksheet =
                workbook.Worksheet(1);

            var rows =
                worksheet.RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                result.TotalRows++;

                try
                {
                    var firstName =
                        row.Cell(1).GetString().Trim();

                    var lastName =
                        row.Cell(2).GetString().Trim();

                    var email =
                        row.Cell(3).GetString().Trim();

                    var phone =
                        row.Cell(4).GetString().Trim();

                    var gender =
                        row.Cell(5).GetString().Trim();

                    if (await _repository.EmailExistsAsync(
                        email,
                        _tenantService.GetTenantId()))
                    {
                        result.Failed++;

                        result.Errors.Add(
                            new BulkUploadErrorDto
                            {
                                RowNumber =
                                    result.TotalRows + 1,
                                Message =
                                    $"Email already exists: {email}"
                            });

                        continue;
                    }
                    var password = PasswordGenerator.Generate(10);
                    List<string> Roles = new List<string>();
                    Roles.Add("Member");
                    User user = new User()
                    {
                        Id =
                        Guid.NewGuid()
                            .ToString(),

                        FullName =
                         firstName + " " + lastName,
                        ChurchId = orgId,
                        Email =
                        email,
                        Roles = Roles,

                        UserName =
                        email,

                        PhoneNumber =
                        phone,

                        IsActive = true
                    };


                   // user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

                    var member = new Member
                    {
                        Id = Guid.NewGuid(),
                        ChurchId =
                            _tenantService.GetTenantId(),

                        MembershipNumber = await 
                            GenerateMembershipNumberAsync(_tenantService.GetTenantId()),

                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        PhoneNumber = phone,
                        Gender = gender,
                        MembershipDate =
                            DateTime.UtcNow
                    };
                    await _userManager.CreateAsync(user, password);
                    await _repository.CreateAsync(member);

                    await _emailService.SendEmailAsync(
               user.Email,
                "Welcome to GHConnect",
                EmailTemplate.WelcomeMember(
            user.FullName,
            "GHConnect",
            user.Email,
            password
                ));
                    result.Successful++;
                }
                catch (Exception ex)
                {
                    result.Failed++;

                    result.Errors.Add(
                        new BulkUploadErrorDto
                        {
                            RowNumber =
                                result.TotalRows + 1,
                            Message = ex.Message
                        });
                }
            }

            return result;
        }
        public async Task<string> GenerateMembershipNumberAsync(
    Guid churchId)
        {
            var church = await _context.Churches
                .FirstOrDefaultAsync(x => x.Id == churchId);

            if (church == null)
                throw new Exception("Church not found");

            church.LastMemberNumber++;

            await _context.SaveChangesAsync();

            return $"MEM-{church.LastMemberNumber:D6}";
        }
        public async Task<List<MemberResponseDto>> GetAllAsync()
            {
                var orgId = _tenantService.GetTenantId();
                var members =
                    await _repository.GetAllAsync(orgId
                        );

                return members.Select(x => new MemberResponseDto
                {
                    Id = x.Id,
                    MembershipNumber = x.MembershipNumber,
                    FirstName =x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    MembershipDate = x.MembershipDate
                }).ToList();
            }

            public async Task<MemberResponseDto?> GetByIdAsync(Guid id)
            {
                var orgId = _tenantService.GetTenantId();
                var member =
                    await _repository.GetByIdAsync(
                        id,
                        orgId);

                if (member == null)
                    return null;

                return new MemberResponseDto
                {
                    Id = member.Id,
                    MembershipNumber = member.MembershipNumber,
                    FirstName =member.FirstName,
                    LastName = member.LastName,
                    PhoneNumber = member.PhoneNumber,
                    Email = member.Email
                };
            }

            public async Task CreateAsync(CreateMemberDto dto)
            {

            var orgId = _tenantService.GetTenantId();

            var password = PasswordGenerator.Generate(10);
            var member = new Member
            {
                Id = Guid.NewGuid(),
                ChurchId = orgId,
                MembershipNumber = $"MEM-{DateTime.UtcNow.Ticks}",
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth.ToUniversalTime(),
                MaritalStatus = dto.MaritalStatus,
                Address = dto.Address,
                Gender = dto.Gender,
                Occupation = dto.Occupation,
                MembershipDate = DateTime.UtcNow
            };

            await _repository.CreateAsync(member);

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                FullName = dto.FirstName + " " + dto.LastName,
                ChurchId = orgId,
                Email = dto.Email,
                Roles = new List<string> { "Member" },
                UserName = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                IsActive = true,
                MemberId = member.Id
            };

            var result =
    await _userManager.CreateAsync(
        user,
        password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(
                    user,
                    "Member");
            }

            await _emailService.SendEmailAsync(
               user.Email,
                "Welcome to GHConnect",
                EmailTemplate.WelcomeMember(
            user.FullName,
            "GHConnect",
            user.Email,
            password
                ));
            }

            public async Task UpdateAsync(
                Guid id,
                UpdateMemberDto dto)
            {
                var orgId = _tenantService.GetTenantId();
                var member =
                    await _repository.GetByIdAsync(
                        id,
                        orgId);

                if (member == null)
                    throw new Exception("Member not found");

                member.FirstName = dto.FirstName;
                member.LastName = dto.LastName;
                member.PhoneNumber = dto.PhoneNumber;
                member.Email = dto.Email;

                await _repository.UpdateAsync(member);
            }

            public async Task DeleteAsync(Guid id)
            {
                var orgId = _tenantService.GetTenantId();
                var member =
                    await _repository.GetByIdAsync(
                        id,
                        orgId);

                if (member == null)
                    throw new Exception("Member not found");

                await _repository.DeleteAsync(member);
            }
        }
    }
