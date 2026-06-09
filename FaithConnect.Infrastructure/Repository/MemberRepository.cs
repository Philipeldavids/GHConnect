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
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
       
        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Member?> GetMemberDetailsAsync(Guid churchId, Guid id)
        {
            return await _context.Members
                .Include(x => x.MemberDepartments)
                    .ThenInclude(x => x.Department)
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.ChurchId == churchId);
        }
        public async Task<List<Attendance>> GetAttendanceHistoryAsync(Guid memberId)
        {
            return await _context.Attendances
                .Include(x => x.Service)
                .Where(x => x.MemberId == memberId)
                .OrderByDescending(x => x.CheckInTime)
                .ToListAsync();
        }

        public async Task<List<CommunicationLog>> GetCommunicationHistoryAsync(Guid memberId)
        {
            return await _context.CommunicationLogs
                .Where(x => x.MemberId == memberId)
                .OrderByDescending(x => x.SentAt)
                .ToListAsync();
        }
        public async Task<bool> EmailExistsAsync(
    string email,
    Guid churchId)
        {
            return await _context.Members.AnyAsync(x =>
                x.ChurchId == churchId &&
                x.Email == email);
        }

        public async Task<bool> PhoneExistsAsync(
            string phone,
            Guid churchId)
        {
            return await _context.Members.AnyAsync(x =>
                x.ChurchId == churchId &&
                x.PhoneNumber == phone);
        }
        public async Task<List<Member>> GetAllAsync(Guid churchId)
        {
            return await _context.Members
                .Where(x => x.ChurchId == churchId)
                .OrderBy(x => x.FirstName)
                .ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(
            Guid id,
            Guid churchId)
        {
            return await _context.Members
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.ChurchId == churchId);
        }

        public async Task<Member?> GetByEmailAsync(
            string email,
            Guid churchId)
        {
            return await _context.Members
                .FirstOrDefaultAsync(x =>
                    x.Email == email &&
                    x.ChurchId == churchId);
        }

        public async Task CreateAsync(Member member)
        {
            _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Member member)
        {
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }
    }
}
