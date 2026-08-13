using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FGScanner.Models;

public partial class InventoryDbContext : DbContext
{
    public InventoryDbContext()
    {
    }

    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActualInventory> ActualInventories { get; set; }

    public virtual DbSet<ActualInventoryClean> ActualInventoryCleans { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Deparment> Deparments { get; set; }

    public virtual DbSet<ErrorTable> ErrorTables { get; set; }

    public virtual DbSet<InventoryRebuildLog> InventoryRebuildLogs { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RackTable> RackTables { get; set; }

    public virtual DbSet<ReturnSequence> ReturnSequences { get; set; }

    public virtual DbSet<ReturnTable> ReturnTables { get; set; }

    public virtual DbSet<ShipmentSequence> ShipmentSequences { get; set; }

    public virtual DbSet<ShipmentTable> ShipmentTables { get; set; }

    public virtual DbSet<StorageLocation> StorageLocations { get; set; }

    public virtual DbSet<SyncbatchTable> SyncbatchTables { get; set; }

    public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

    public virtual DbSet<TransactionHistoryClean> TransactionHistoryCleans { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<UserInformation> UserInformations { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActualInventory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("actual_inventory");

            entity.HasIndex(e => new { e.WhId, e.Customer, e.Partnumber, e.ProdVer, e.ProdDate, e.Location, e.StorageLocation }, "IX_inventory_unique").IsUnique();

            entity.HasIndex(e => new { e.WhId, e.Partnumber, e.ProdVer, e.Customer, e.ProdDate, e.Location, e.StorageLocation }, "UQ_inventory").IsUnique();

            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IdleDays).HasColumnName("idle_days");
            entity.Property(e => e.LastInDate)
                .HasColumnType("datetime")
                .HasColumnName("last_in_date");
            entity.Property(e => e.LastOutDate)
                .HasColumnType("datetime")
                .HasColumnName("last_out_date");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .HasColumnName("location");
            entity.Property(e => e.MovementClassification)
                .HasMaxLength(50)
                .HasColumnName("movement_classification");
            entity.Property(e => e.Partnumber)
                .HasMaxLength(50)
                .HasColumnName("partnumber");
            entity.Property(e => e.ProdDate).HasColumnName("prod_date");
            entity.Property(e => e.ProdVer)
                .HasMaxLength(50)
                .HasColumnName("prod_ver");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .HasColumnName("remarks");
            entity.Property(e => e.StorageLocation)
                .HasMaxLength(50)
                .HasColumnName("storage_location");
            entity.Property(e => e.TotalBox).HasColumnName("total_box");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
            entity.Property(e => e.WhId).HasMaxLength(50);
        });

        modelBuilder.Entity<ActualInventoryClean>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("actual_inventory_clean");

            entity.HasIndex(e => new { e.WhId, e.Partnumber, e.ProdVer, e.Customer, e.ProdDate, e.Location, e.StorageLocation }, "UQ_inventory_clean").IsUnique();

            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .HasColumnName("customer");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IdleDays).HasColumnName("idle_days");
            entity.Property(e => e.LastInDate)
                .HasColumnType("datetime")
                .HasColumnName("last_in_date");
            entity.Property(e => e.LastOutDate)
                .HasColumnType("datetime")
                .HasColumnName("last_out_date");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .HasColumnName("location");
            entity.Property(e => e.MovementClassification)
                .HasMaxLength(50)
                .HasColumnName("movement_classification");
            entity.Property(e => e.Partnumber)
                .HasMaxLength(50)
                .HasColumnName("partnumber");
            entity.Property(e => e.ProdDate).HasColumnName("prod_date");
            entity.Property(e => e.ProdVer)
                .HasMaxLength(50)
                .HasColumnName("prod_ver");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .HasColumnName("remarks");
            entity.Property(e => e.StorageLocation)
                .HasMaxLength(50)
                .HasColumnName("storage_location");
            entity.Property(e => e.TotalBox).HasColumnName("total_box");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
            entity.Property(e => e.WhId).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB8573E64759");

            entity.ToTable("customer");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .HasColumnName("customer_name");
        });

        modelBuilder.Entity<Deparment>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__deparmen__DCA65974A0C9848C");

            entity.ToTable("deparment");

            entity.Property(e => e.DeptId)
                .HasMaxLength(255)
                .HasColumnName("dept_id");
            entity.Property(e => e.DeptGroup)
                .HasMaxLength(255)
                .HasColumnName("dept_group");
        });

        modelBuilder.Entity<ErrorTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Error_Table");

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasColumnName("message");
            entity.Property(e => e.Stacktrace)
                .IsRequired()
                .HasColumnName("stacktrace");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");
        });

        modelBuilder.Entity<InventoryRebuildLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("InventoryRebuildLog");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.RebuildDate).HasColumnType("datetime");
            entity.Property(e => e.RebuiltBy)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("PK__Modules__1A2D06535EF8ED8F");

            entity.Property(e => e.ModuleId).HasColumnName("module_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.ModuleName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("module_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__product__3213E83F0825440C");

            entity.ToTable("product");

            entity.HasIndex(e => e.Partnumber, "UQ__product__4D9CAFD8A324E049").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(255)
                .HasColumnName("customer_id");
            entity.Property(e => e.Partname)
                .HasMaxLength(255)
                .HasColumnName("partname");
            entity.Property(e => e.Partnumber)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("partnumber");
            entity.Property(e => e.Pps).HasColumnName("PPS");
        });

        modelBuilder.Entity<RackTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("rack_table");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.RackId)
                .HasMaxLength(50)
                .HasColumnName("rack_id");
            entity.Property(e => e.RackNo)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("rack_no");
            entity.Property(e => e.WhId)
                .HasMaxLength(50)
                .HasColumnName("wh_id");
        });

        modelBuilder.Entity<ReturnSequence>(entity =>
        {
            entity.HasKey(e => e.SeqDate);

            entity.ToTable("return_sequence");

            entity.Property(e => e.SeqDate).HasColumnName("seq_date");
            entity.Property(e => e.LastNumber).HasColumnName("last_number");
        });

        modelBuilder.Entity<ReturnTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Return_table");

            entity.HasIndex(e => e.ReturnId, "UQ_ReturnId").IsUnique();

            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("entry_date");
            entity.Property(e => e.FromLocation)
                .HasMaxLength(50)
                .HasColumnName("fromLocation");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsSynced).HasDefaultValue(false);
            entity.Property(e => e.Remarks).HasColumnName("remarks");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.ToLocation)
                .HasMaxLength(50)
                .HasColumnName("toLocation");
            entity.Property(e => e.TransactionId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("transaction_id");
            entity.Property(e => e.WhId).HasMaxLength(50);
        });

        modelBuilder.Entity<ShipmentSequence>(entity =>
        {
            entity.HasKey(e => e.SeqDate).HasName("PK__shipment__0A2449D5F13AB611");

            entity.ToTable("shipment_sequence");

            entity.Property(e => e.SeqDate).HasColumnName("seq_date");
            entity.Property(e => e.LastNumber).HasColumnName("last_number");
        });

        modelBuilder.Entity<ShipmentTable>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK_Shipments");

            entity.ToTable("Shipment_table");

            entity.Property(e => e.TransactionId)
                .HasMaxLength(50)
                .HasColumnName("transaction_id");
            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("entry_date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsSynced).HasDefaultValue(false);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.WhId).HasMaxLength(50);
        });

        modelBuilder.Entity<StorageLocation>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("storage_location");

            entity.Property(e => e.ErpLoc)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("erp_loc");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<SyncbatchTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Syncbatch_table");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.WhId)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<TransactionHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__transact__3213E83F3956D54D");

            entity.ToTable("transaction_history", tb => tb.HasTrigger("trg_UpdateInventory"));

            entity.HasIndex(e => e.TransactionId, "UQ_TransactionId").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ControlNumber)
                .HasMaxLength(50)
                .HasColumnName("control_number");
            entity.Property(e => e.CustomerId)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("customer_id");
            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("entry_date");
            entity.Property(e => e.InCharge)
                .HasMaxLength(50)
                .HasColumnName("in_charge");
            entity.Property(e => e.IsSynced).HasDefaultValue(false);
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("location");
            entity.Property(e => e.Partnumber)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("partnumber");
            entity.Property(e => e.ProdDate).HasColumnName("prod_date");
            entity.Property(e => e.ProdVer)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("prod_ver");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .HasColumnName("remarks");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.StorageLocation)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("storage_location");
            entity.Property(e => e.TransactionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.TransactionType)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("transaction_type");
            entity.Property(e => e.WhId)
                .HasMaxLength(50)
                .HasColumnName("WH_id");
        });

        modelBuilder.Entity<TransactionHistoryClean>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("transaction_history_clean", tb => tb.HasTrigger("trg_UpdateInventory_clean"));

            entity.Property(e => e.ControlNumber)
                .HasMaxLength(50)
                .HasColumnName("control_number");
            entity.Property(e => e.CustomerId)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("customer_id");
            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("entry_date");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.InCharge)
                .HasMaxLength(50)
                .HasColumnName("in_charge");
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("location");
            entity.Property(e => e.Partnumber)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("partnumber");
            entity.Property(e => e.ProdDate).HasColumnName("prod_date");
            entity.Property(e => e.ProdVer)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("prod_ver");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Remarks)
                .HasMaxLength(50)
                .HasColumnName("remarks");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.StorageLocation)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("storage_location");
            entity.Property(e => e.TransactionType)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("transaction_type");
            entity.Property(e => e.WhId)
                .HasMaxLength(50)
                .HasColumnName("WH_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F9BA9F624");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserId, "UQ__users__B9BE370E2B4B6C19").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.GroupCategory)
                .HasMaxLength(50)
                .HasColumnName("group_category");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.LastActive)
                .HasColumnType("datetime")
                .HasColumnName("last_active");
            entity.Property(e => e.LastLoginDate)
                .HasColumnType("datetime")
                .HasColumnName("last_login_date");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Group).WithMany(p => p.Users)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_users_group");
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__user_gro__D57795A066E9B7AF");

            entity.ToTable("user_group");

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.GroupName)
                .HasMaxLength(255)
                .HasColumnName("group_name");
        });

        modelBuilder.Entity<UserInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_inf__3213E83F7BC77730");

            entity.ToTable("user_information");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(255)
                .HasColumnName("department_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Department).WithMany(p => p.UserInformations)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_userinfo_department");

            entity.HasOne(d => d.Group).WithMany(p => p.UserInformations)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_userinfo_group");

            entity.HasOne(d => d.User).WithMany(p => p.UserInformations)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_userinfo_user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
