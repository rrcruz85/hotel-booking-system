﻿// <auto-generated />
using System;
using Hotel.Management.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hotel.Management.DataAccess.Migrations
{
    [DbContext(typeof(HotelManagementContext))]
    partial class HotelManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Hotel.Management.DataAccess.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("City", (string)null);
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LocationId");

                    b.ToTable("Hotel", (string)null);
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.HotelCategory", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("HotelCategory", (string)null);
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.HotelContactInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasComment("possible values are: email (1), mobile (2), phone (3), website (4), social network (5)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelContactInfo", (string)null);
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.HotelFacility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1024)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelFacility", (string)null);
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.HotelGallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BlobImageUri")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2048)");

                    b.Property<string>("Description")
                        .HasMaxLength(2048)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2048)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelGallery", (string)null);
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.HotelService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(2048)
                        .IsUnicode(false)
                        .HasColumnType("varchar(2048)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelService", (string)null);
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("GeoLocation")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Location", (string)null);
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("money");

                    b.Property<string>("Extension")
                        .HasMaxLength(16)
                        .IsUnicode(false)
                        .HasColumnType("varchar(16)");

                    b.Property<int?>("Floor")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(32)
                        .IsUnicode(false)
                        .HasColumnType("varchar(32)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Room", (string)null);
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.Hotel", b =>
                {
                    b.HasOne("Hotel.Management.DataAccess.HotelCategory", "Category")
                        .WithMany("Hotels")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Category");

                    b.HasOne("Hotel.Management.DataAccess.Location", "Location")
                        .WithMany("Hotels")
                        .HasForeignKey("LocationId")
                        .IsRequired()
                        .HasConstraintName("FK_Location");

                    b.Navigation("Category");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.HotelContactInfo", b =>
                {
                    b.HasOne("Hotel.Management.DataAccess.Hotel", "Hotel")
                        .WithMany("HotelContactInfos")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_ContactInfo_Hotel");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.HotelFacility", b =>
                {
                    b.HasOne("Hotel.Management.DataAccess.Hotel", "Hotel")
                        .WithMany("HotelFacilities")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Hotel");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.HotelGallery", b =>
                {
                    b.HasOne("Hotel.Management.DataAccess.Hotel", "Hotel")
                        .WithMany("HotelGalleries")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_HotelGallery_Hotel");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.HotelService", b =>
                {
                    b.HasOne("Hotel.Management.DataAccess.Hotel", "Hotel")
                        .WithMany("HotelServices")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_HotelService_Hotel");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.Location", b =>
                {
                    b.HasOne("Hotel.Management.DataAccess.City", "City")
                        .WithMany("Locations")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_City");

                    b.Navigation("City");
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.Room", b =>
                {
                    b.HasOne("Hotel.Management.DataAccess.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Hotel_Room");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.City", b =>
                {
                    b.Navigation("Locations");
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.Hotel", b =>
                {
                    b.Navigation("HotelContactInfos");

                    b.Navigation("HotelFacilities");

                    b.Navigation("HotelGalleries");

                    b.Navigation("HotelServices");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.HotelCategory", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("Hotel.Management.DataAccess.Location", b =>
                {
                    b.Navigation("Hotels");
                });
#pragma warning restore 612, 618
        }
    }
}
