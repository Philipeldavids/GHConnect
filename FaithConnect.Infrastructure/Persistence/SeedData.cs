using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static async Task SeedRolesAsync(
            RoleManager<Role> roleManager)
        {
            string[] roles =
            [
                "SuperAdmin",
            "ChurchAdmin",
            "Pastor",
            "DepartmentHead",
            "AttendanceOfficer",
            "CommunicationOfficer",
            "Member"
            ];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new Role
                        {
                            Name = role,
                            NormalizedName =
                                role.ToUpper()
                        });
                }
            }
        }

        
        
    }
}