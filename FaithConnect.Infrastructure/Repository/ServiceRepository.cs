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
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Service>> GetAllAsync(Guid churchId)
        {
            return await _context.Services
                .Where(x => x.ChurchId == churchId)
                .OrderByDescending(x => x.ServiceDate)
                .ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _context.Services
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Service?> GetActiveServiceAsync(Guid churchId)
        {
            var now = DateTime.Now;

            return await _context.Services
                .FirstOrDefaultAsync(x =>
                    x.ChurchId == churchId &&
                    x.AttendanceEnabled &&
                    x.ServiceDate.Date == now.Date);
        }

        public async Task CreateAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var service =
                await _context.Services.FindAsync(id);

            if (service == null)
                return;

            _context.Services.Remove(service);

            await _context.SaveChangesAsync();
        }
    }
}
