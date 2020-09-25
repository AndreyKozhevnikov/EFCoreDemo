using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp.EFCore.Updating;
using DevExpress.Persistent.BaseImpl;
using EFCoreDemo.Module.BusinessObjects;
using Microsoft.EntityFrameworkCore;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;

namespace EFCoreDemo.Module.BusinessObjects {
    public class EFCoreDemoDbContext : DbContext {
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            ConfigureDefaultXafClasses(modelBuilder);
            modelBuilder.Entity<EFCoreDemo.Module.BusinessObjects.FileData>().ToTable("FileData");

            modelBuilder.Entity<ContactDemoTask>()
                .HasKey(e => new { e.ContactID, e.TaskID });

            modelBuilder.Entity<ContactDemoTask>()
                .HasOne(e => e.Contact)
                .WithMany(e => e.ContactTasks)
                .HasForeignKey(e => e.ContactID);

            modelBuilder.Entity<ContactDemoTask>()
                .HasOne(e => e.Task)
                .WithMany(e => e.TaskContacts)
                .HasForeignKey(e => e.TaskID);
            modelBuilder.Entity<DepartmentPosition>()
                .HasOne(e => e.Department)
                .WithMany(e => e.DepartmentPositions)
                .HasForeignKey(e => e.DepartmentID);

            modelBuilder.Entity<DepartmentPosition>()
                .HasOne(e => e.Position)
                .WithMany(e => e.PositionDepartments)
                .HasForeignKey(e => e.PositionID);

            modelBuilder.Entity<Resume>()
                .HasMany(r => r.Portfolio)
                .WithOne(p => p.Resume)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Party>()
                .HasMany(r => r.PhoneNumbers)
                .WithOne(p => p.Party)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contact>()
                .HasOne(r => r.Location)
                .WithOne(p => p.Contact)
                .HasForeignKey<Location>(fk => fk.ContactRef);
            modelBuilder.Entity<Department>()
                .HasMany(p => p.Contacts)
                .WithOne(r => r.Department);

            modelBuilder.Entity<Department>()
                .HasOne(r => r.DepartmentHead);
        }

        public EFCoreDemoDbContext(DbContextOptions<EFCoreDemoDbContext> options)
            : base(options) {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Paycheck> Paycheck { get; set; }
        public DbSet<FileAttachment> FileAttachments { get; set; }
        public DbSet<FileData> FileData { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Task> Tasks { get; set; }


        #region Default XAF Configurations
        public DbSet<PermissionPolicyUser> Users { get; set; }
        public DbSet<ModelDifference> ModelDifferences { get; set; }
        public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
        public DbSet<PermissionPolicyRole> Roles { get; set; }
        public DbSet<ModuleInfo> ModulesInfo { get; set; }
        private void ConfigureDefaultXafClasses(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ModuleInfo>().ToTable("ModulesInfo");

            modelBuilder.Entity<PermissionPolicyUserRole>()
                .HasKey(e => new { e.RoleID, e.UserID });

            modelBuilder.Entity<PermissionPolicyUserRole>()
               .HasOne(e => e.Role)
               .WithMany(e => e.RoleUsers)
               .HasForeignKey(e => e.RoleID);

            modelBuilder.Entity<PermissionPolicyUserRole>()
                .HasOne(e => e.User)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.UserID);
            modelBuilder.Entity<PermissionPolicyRoleBase>()
               .HasMany(r => r.TypePermissions)
               .WithOne(p => p.Role)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PermissionPolicyRoleBase>()
               .HasMany(r => r.NavigationPermissions)
               .WithOne(p => p.Role)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PermissionPolicyTypePermissionObject>()
               .HasMany(r => r.MemberPermissions)
               .WithOne(p => p.TypePermissionObject)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PermissionPolicyTypePermissionObject>()
               .HasMany(r => r.ObjectPermissions)
               .WithOne(p => p.TypePermissionObject)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ModelDifference>()
              .HasMany(r => r.Aspects)
              .WithOne(p => p.Owner)
              .OnDelete(DeleteBehavior.Cascade);
        }
        #endregion
    }
}
