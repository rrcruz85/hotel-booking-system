using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Reservation.Management.DataAccess.Entities;

namespace Reservation.Management.DataAccess
{
    public partial class ReservationManagementContext : DbContext
    {
        protected readonly IConfiguration _config;

        public ReservationManagementContext(IConfiguration config, DbContextOptions<ReservationManagementContext> options): base(options)
        {
            _config = config;
        }

        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Entities.Hotel> Hotels { get; set; } = null!;
        public virtual DbSet<HotelCategory> HotelCategories { get; set; } = null!;
        public virtual DbSet<HotelCategoryRelation> HotelCategoryRelations { get; set; } = null!;
        public virtual DbSet<HotelContactInfo> HotelContactInfos { get; set; } = null!;
        public virtual DbSet<HotelFacility> HotelFacilities { get; set; } = null!;
        public virtual DbSet<HotelGallery> HotelGalleries { get; set; } = null!;
        public virtual DbSet<HotelService> HotelServices { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Entities.Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<ReservationHistory> ReservationHistories { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RolePermission> RolePermissions { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomReservation> RoomReservations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserContactInfo> UserContactInfos { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                // connect to sql server with connection string from app settings
                options.UseSqlServer(_config.GetConnectionString("ReservationManagement"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.HasIndex(e => e.Name, "NonClusteredIndex-Name-State-Country");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Country)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Entities.Hotel>(entity =>
            {
                entity.ToTable("Hotel");

                entity.HasIndex(e => e.Name, "NonClusteredIndex-Name-CityId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GeoLocation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hotel_City");
            });

            modelBuilder.Entity<HotelCategory>(entity =>
            {
                entity.ToTable("HotelCategory");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HotelCategoryRelation>(entity =>
            {
                entity.ToTable("HotelCategoryRelation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.HotelCategory)
                    .WithMany(p => p.HotelCategoryRelations)
                    .HasForeignKey(d => d.HotelCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HotelCategoryRelation_HotelCategory");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.HotelCategoryRelations)
                    .HasForeignKey(d => d.HotelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HotelCategoryRelation_Hotel");
            });

            modelBuilder.Entity<HotelContactInfo>(entity =>
            {
                entity.ToTable("HotelContactInfo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Value)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.HotelContactInfos)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_ContactInfo_Hotel");
            });

            modelBuilder.Entity<HotelFacility>(entity =>
            {
                entity.ToTable("HotelFacility");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.HotelFacilities)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Hotel");
            });

            modelBuilder.Entity<HotelGallery>(entity =>
            {
                entity.ToTable("HotelGallery");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BlobImageUri)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.HotelGalleries)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_HotelGallery_Hotel");
            });

            modelBuilder.Entity<HotelService>(entity =>
            {
                entity.ToTable("HotelService");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.HotelServices)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_HotelService_Hotel");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.HasIndex(e => e.ReservationId, "NonClusteredIndex-HotelId-Status-IssuedAt");

                entity.Property(e => e.CanceledAt).HasColumnType("datetime");

                entity.Property(e => e.IssuedAt).HasColumnType("datetime");

                entity.Property(e => e.Observations)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.PaidAt).HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnType("money");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Reservation");
            });

            modelBuilder.Entity<Entities.Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.HasIndex(e => e.Status, "NonClusteredIndex-StartDate-EndDate-Status-UserId");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Observations)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentMethodInfo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservation_User");
            });

            modelBuilder.Entity<ReservationHistory>(entity =>
            {
                entity.ToTable("Reservation_History");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(1024)
                    .IsUnicode(false);                
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("RolePermission");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_RolePermission_Role");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.HasIndex(e => e.HotelId, "NonClusteredIndex-HotelId-Status-Number-Type");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CurrentPrice).HasColumnType("money");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Room_Hotel");
            });

            modelBuilder.Entity<RoomReservation>(entity =>
            {
                entity.ToTable("RoomReservation");

                entity.Property(e => e.DiscountPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Discount_Price");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.RoomReservations)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomReservation_Reservation");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomReservations)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomReservation_Room");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<UserContactInfo>(entity =>
            {
                entity.ToTable("UserContactInfo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Value)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.UserContactInfos)
                    .HasForeignKey(d => d.ProfileId)
                    .HasConstraintName("FK_ContactInfo_Profile");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("UserProfile");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IdValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProfile_City");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserProfile_User");
            });

            modelBuilder.Entity<City>().HasData(new City { Id = 1, Country = "Mexico", State = "Merida", Name = "Cancum" });
            modelBuilder.Entity<Entities.Hotel>().HasData(new Entities.Hotel { Id = 1, Name = "Grand Carribean", AddressLine1 = "Street 1", CityId = 1 });
            modelBuilder.Entity<Room>().HasData(new Room { Id = 1, HotelId = 1, Number = 101, MaxCapacity = 2, Floor = 1, Status = 1, CurrentPrice = 50, Type = 1 });
            modelBuilder.Entity<Room>().HasData(new Room { Id = 2, HotelId = 1, Number = 102, MaxCapacity = 4, Floor = 1, Status = 1, CurrentPrice = 100, Type = 2 });
            modelBuilder.Entity<Room>().HasData(new Room { Id = 3, HotelId = 1, Number = 103, MaxCapacity = 4, Floor = 1, Status = 1, CurrentPrice = 150, Type = 2 });
            modelBuilder.Entity<Room>().HasData(new Room { Id = 4, HotelId = 1, Number = 106, MaxCapacity = 6, Floor = 1, Status = 1, CurrentPrice = 200, Type = 3 });
            modelBuilder.Entity<Room>().HasData(new Room { Id = 5, HotelId = 1, Number = 107, MaxCapacity = 6, Floor = 1, Status = 1, CurrentPrice = 250, Type = 3 });

            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name ="Customer" });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Manager" });
            modelBuilder.Entity<User>().HasData(new User { Id = 1, IsActive = true, Password = "123456", UserName = "user", RoleId = 1 });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
