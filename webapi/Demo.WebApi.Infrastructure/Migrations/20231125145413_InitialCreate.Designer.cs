﻿// <auto-generated />
using Demo.WebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Demo.WebApi.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231125145413_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Demo.WebApi.Core.Entities.Addresses.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddressLine")
                        .IsRequired()
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsUSA")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ZipCode")
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("AppAddresses", (string)null);
                });

            modelBuilder.Entity("Demo.WebApi.Core.Entities.Clients.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.HasKey("Id");

                    b.ToTable("AppClients", (string)null);
                });

            modelBuilder.Entity("Demo.WebApi.Core.Entities.Addresses.Address", b =>
                {
                    b.HasOne("Demo.WebApi.Core.Entities.Clients.Client", "Client")
                        .WithMany("Addresses")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Demo.WebApi.Core.Entities.Clients.Client", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
