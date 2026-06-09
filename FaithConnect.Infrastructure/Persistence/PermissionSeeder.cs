using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Infrastructure.Persistence
{
    public static class PermissionSeeder
    {
        public static async Task SeedAsync(
            ApplicationDbContext context)
        {
            if (
                context.Permissions.Any()
            )
                return;

            var permissions =
                new List<Permission>
                {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name =
                        "MEMBER_VIEW"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name =
                        "MEMBER_CREATE"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name =
                        "MEMBER_EDIT"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name =
                        "MEMBER_DELETE"
                },

                new()
                {
                    Id = Guid.NewGuid(),
                    Name =
                        "ATTENDANCE_VIEW"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name =
                        "ATTENDANCE_MARK"
                },

                new()
                {
                    Id = Guid.NewGuid(),
                    Name =
                        "COMMUNICATION_SEND"
                },

                new()
                {
                    Id = Guid.NewGuid(),
                    Name =
                        "DEPARTMENT_MANAGE"
                },

                new()
                {
                    Id = Guid.NewGuid(),
                    Name =
                        "USER_MANAGE"
                },

                new()
                {
                    Id = Guid.NewGuid(),
                    Name =
                        "ROLE_MANAGE"
                }
                };

            await context
                .Permissions
                .AddRangeAsync(
                    permissions);

            await context
                .SaveChangesAsync();
        }
    }
}