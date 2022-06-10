﻿// <auto-generated />
using System;
using ApiApplication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220610090620_mi")]
    partial class mi
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.4.22229.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BotClassLibrary.Rappel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscordMember")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RappelDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("_rappels");
                });

            modelBuilder.Entity("BotClassLibrary.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscordMember")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeal")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("_tickets");
                });

            modelBuilder.Entity("BusClassLibrary.Arret", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("stop_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ville")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("xlocation")
                        .HasColumnType("real");

                    b.Property<float>("ylocation")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Arret");
                });

            modelBuilder.Entity("BusClassLibrary.Ligne", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("route_desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("route_short_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("route_text_color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("route_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("_lignes");
                });

            modelBuilder.Entity("BusClassLibrary.Shape", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("lat")
                        .HasColumnType("real");

                    b.Property<float>("longit")
                        .HasColumnType("real");

                    b.Property<int>("sequence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("_shapes");
                });

            modelBuilder.Entity("BusClassLibrary.StopTimes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("arrival_time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("departure_time")
                        .HasColumnType("datetime2");

                    b.Property<int>("stop_id")
                        .HasColumnType("int");

                    b.Property<float>("stop_sequence")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("_stopTimes");
                });

            modelBuilder.Entity("BusClassLibrary.Trip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("direction_id")
                        .HasColumnType("int");

                    b.Property<Guid>("service_id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("shape_id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("trip_headsign")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("_trips");
                });
#pragma warning restore 612, 618
        }
    }
}
