using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GPSsystem.Data
{
    public partial class gpssystemContext : DbContext
    {
        public gpssystemContext()
        {
        }

        public gpssystemContext(DbContextOptions<gpssystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AvaliableDriver> AvaliableDrivers { get; set; }
        public virtual DbSet<CurrentPo> CurrentPos { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Hospital> Hospitals { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                bool isProduction = false;
                string dbConn = string.Empty;
                if (isProduction)
                {
                    dbConn = "Server=tcp:gps-prof-db.database.windows.net,1433;Initial Catalog=gpssystem;Persist Security Info=False;User ID=azamat;Password=Turar2380;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                }
                else
                {
                    dbConn = "Server=DESKTOP-16OT93O;Database=gpssystem;Trusted_Connection=True;";
                }
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(dbConn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvaliableDriver>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AvaliableDrivers");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.HospitalId).HasColumnName("hospital_id");

                entity.Property(e => e.Imei)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("imei");
            });

            modelBuilder.Entity<CurrentPo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CurrentPos");

                entity.Property(e => e.AddressPos)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("address_pos");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.DriverPos)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("driver_pos");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("drivers");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.HospitalId).HasColumnName("hospital_id");

                entity.Property(e => e.Imei)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("imei");

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__drivers__hospita__01142BA1");
            });

            modelBuilder.Entity<Hospital>(entity =>
            {
                entity.ToTable("hospitals");

                entity.Property(e => e.HospitalId).HasColumnName("hospital_id");

                entity.Property(e => e.HospitalAddress)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("hospital_address");

                entity.Property(e => e.HospitalName)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("hospital_name");

                entity.Property(e => e.HospitalPos)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("hospital_pos");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("locations");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.Pos)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("pos");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("requests");

                entity.Property(e => e.RequestId).HasColumnName("request_id");

                entity.Property(e => e.AddressName)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("address_name");

                entity.Property(e => e.AddressPos)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("address_pos");

                entity.Property(e => e.ArrivalTime)
                    .HasColumnType("datetime")
                    .HasColumnName("arrival_time");

                entity.Property(e => e.BackTime)
                    .HasColumnType("datetime")
                    .HasColumnName("back_time");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("client_name");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__requests__driver__07C12930");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("token");

                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Token1)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("token");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
