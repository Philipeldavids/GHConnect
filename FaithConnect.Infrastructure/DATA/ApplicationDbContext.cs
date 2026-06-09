using FaithConnect.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Infrastructure.DATA
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Church> Churches { get; set; }
        public DbSet<Household> Families { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users {  get; set; }
       
        public DbSet<SmsCampaign> SmsCampaigns { get; set; }
        public DbSet<EmailCampaign> EmailCampaigns { get; set; }
       
        public DbSet<CommunicationLog> CommunicationLogs { get; set; }
        public DbSet<MemberDepartment> MemberDepartments { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<ChurchSettings> ChurchSettings { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>()
    .HasIndex(x => new
    {
        x.ChurchId,
        x.Name
    })
    .IsUnique();
            modelBuilder.Entity<MemberDepartment>()
    .HasIndex(x => new
    {
        x.MemberId,
        x.DepartmentId
    })
    .IsUnique();

            modelBuilder.Entity<User>()
       .HasOne(x => x.Member)
       .WithMany()
       .HasForeignKey(x => x.MemberId)
       .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
      .HasOne(x => x.Church)
      .WithMany()
      .HasForeignKey(x => x.ChurchId)
      .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e=>e.Church)
                .WithMany(e => e.Members)
                .HasForeignKey(e => e.ChurchId)
                .OnDelete(DeleteBehavior.Cascade);

                

                entity.HasOne(x=>x.Household)
                .WithMany(x=>x.Members)
                .HasForeignKey(x=>x.FamilyId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MemberDepartment>(entity =>
            {
                entity.HasOne(md => md.Member)
                    .WithMany(m => m.MemberDepartments)
                    .HasForeignKey(md => md.MemberId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(md => md.Department)
                    .WithMany(d => d.MemberDepartments)
                    .HasForeignKey(md => md.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.UserId);
            });
            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(x => x.Id);                
            } );

            modelBuilder.Entity<Church>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            modelBuilder.Entity<Household>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            //modelBuilder.Entity<Role>(entity =>
            //{
            //    entity.HasKey(x => x.Id);
            //});

            modelBuilder.Entity<SmsCampaign>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
           
            modelBuilder.Entity<CommunicationLog>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            modelBuilder.Entity<EmailCampaign>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<RolePermission>()
    .HasKey(x => new
    {
        x.RoleId,
        x.PermissionId
    });

            modelBuilder.Entity<RolePermission>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(x => x.Permission)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.PermissionId);
        }

    }
}



