using System;
using System.Collections.Generic;
using Hotel.Management.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hotel.Management.DataAccess
{
    public partial class HotelManagementContext : DbContext
    {
        public HotelManagementContext()
        {
        }

        public HotelManagementContext(DbContextOptions<HotelManagementContext> options) : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Entities.Hotel> Hotels { get; set; } = null!;
        public virtual DbSet<HotelCategory> HotelCategories { get; set; } = null!;
        public virtual DbSet<HotelContactInfo> HotelContactInfos { get; set; } = null!;
        public virtual DbSet<HotelFacility> HotelFacilities { get; set; } = null!;
        public virtual DbSet<HotelGallery> HotelGalleries { get; set; } = null!;
        public virtual DbSet<HotelService> HotelServices { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Entities.Hotel>(entity =>
            {
                entity.ToTable("Hotel");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Category");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location");
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

            modelBuilder.Entity<HotelContactInfo>(entity =>
            {
                entity.ToTable("HotelContactInfo");

                entity.Property(e => e.Type).HasComment("possible values are: email (1), mobile (2), phone (3), website (4), social network (5)");

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

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GeoLocation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_City");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.CurrentPrice).HasColumnType("money");

                entity.Property(e => e.Extension)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Number)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Hotel_Room");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
