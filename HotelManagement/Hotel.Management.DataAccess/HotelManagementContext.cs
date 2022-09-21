using System;
using System.Collections.Generic;
using Hotel.Management.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Hotel.Management.DataAccess
{
    public partial class HotelManagementContext : DbContext
    {
        protected readonly IConfiguration _config;

        public HotelManagementContext(IConfiguration config, DbContextOptions options) : base(options)
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
        public virtual DbSet<Room> Rooms { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

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

                entity.Property(e => e.Id);

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

                entity.Property(e => e.Id);

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

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.Id);

                entity.Property(e => e.CurrentPrice).HasColumnType("money");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Room_Hotel");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(_config.GetConnectionString("HotelManagement"));
        }
    }
}
