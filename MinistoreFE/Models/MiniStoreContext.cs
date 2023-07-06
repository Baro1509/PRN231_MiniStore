using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MinistoreFE.Models
{
    public partial class MiniStoreContext : DbContext
    {
        public MiniStoreContext()
        {
        }

        public MiniStoreContext(DbContextOptions<MiniStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Duty> Duties { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public virtual DbSet<LeaveRequest> LeaveRequests { get; set; } = null!;
        public virtual DbSet<MonthSalary> MonthSalaries { get; set; } = null!;
        public virtual DbSet<MonthlyBonus> MonthlyBonus { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ShiftSalary> ShiftSalaries { get; set; } = null!;
        public virtual DbSet<WorkShift> WorkShifts { get; set; } = null!;
        public virtual DbSet<Staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=(local);database=MiniStore;uid=sa;pwd=1;TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("Attendance");

                entity.Property(e => e.AttendanceId).HasColumnName("AttendanceID");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.IsCheckIn).HasDefaultValueSql("((0))");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Attendanc__Staff__45BE5BA9");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CategoryID");

                entity.Property(e => e.CategoryDescription).HasMaxLength(150);

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Duty>(entity =>
            {
                entity.ToTable("Duty");

                entity.Property(e => e.DutyId).HasColumnName("DutyID");

                entity.Property(e => e.AssignedTo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.Duties)
                    .HasForeignKey(d => d.AssignedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Duty__AssignedTo__2EDAF651");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.Duties)
                    .HasForeignKey(d => d.ShiftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Duty__ShiftID__2DE6D218");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Invoice__StaffID__208CD6FA");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceId, e.ProductId })
                    .HasName("PK__InvoiceD__1CD666BBCF7F513E");

                entity.ToTable("InvoiceDetail");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InvoiceDe__Invoi__4F47C5E3");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InvoiceDe__Produ__503BEA1C");
            });

            modelBuilder.Entity<LeaveRequest>(entity =>
            {
                entity.ToTable("LeaveRequest");

                entity.Property(e => e.LeaveRequestId).HasColumnName("LeaveRequestID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.RequestedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.LeaveRequestApprovedByNavigations)
                    .HasForeignKey(d => d.ApprovedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LeaveRequ__Appro__41EDCAC5");

                entity.HasOne(d => d.RequestedByNavigation)
                    .WithMany(p => p.LeaveRequestRequestedByNavigations)
                    .HasForeignKey(d => d.RequestedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LeaveRequ__Reque__40F9A68C");
            });

            modelBuilder.Entity<MonthSalary>(entity =>
            {
                entity.ToTable("MonthSalary");

                entity.Property(e => e.MonthSalaryId).HasColumnName("MonthSalaryID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AssignedTo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.MonthSalaryApprovedByNavigations)
                    .HasForeignKey(d => d.ApprovedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MonthSala__Appro__59C55456");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.MonthSalaryAssignedToNavigations)
                    .HasForeignKey(d => d.AssignedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MonthSala__Assig__58D1301D");
            });

            modelBuilder.Entity<MonthlyBonus>(entity =>
            {
                entity.HasKey(e => e.MonthlyBonusId)
                    .HasName("PK__MonthlyB__2C75704C5D5946C5");

                entity.Property(e => e.MonthlyBonusId).HasColumnName("MonthlyBonusID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AssignedTo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Bonus).HasColumnType("money");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.MonthlyBonuApprovedByNavigations)
                    .HasForeignKey(d => d.ApprovedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MonthlyBo__Appro__3D2915A8");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.MonthlyBonuAssignedToNavigations)
                    .HasForeignKey(d => d.AssignedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MonthlyBo__Assig__3C34F16F");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CategoryID");

                entity.Property(e => e.Description)
                    .HasMaxLength(220)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Categor__1CBC4616");
            });

            modelBuilder.Entity<ShiftSalary>(entity =>
            {
                entity.ToTable("ShiftSalary");

                entity.Property(e => e.ShiftSalaryId).HasColumnName("ShiftSalaryID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AssignedTo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.ShiftSalaryApprovedByNavigations)
                    .HasForeignKey(d => d.ApprovedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShiftSala__Appro__55009F39");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.ShiftSalaryAssignedToNavigations)
                    .HasForeignKey(d => d.AssignedTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShiftSala__Assig__540C7B00");
            });

            modelBuilder.Entity<WorkShift>(entity =>
            {
                entity.HasKey(e => e.ShiftId)
                    .HasName("PK__WorkShif__C0A838E1816E463B");

                entity.ToTable("WorkShift");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.Bonus)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Coefficient).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.WorkShifts)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkShift__Creat__2A164134");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID");

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("RoleID");

                entity.Property(e => e.StaffName)
                    .HasMaxLength(180)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
