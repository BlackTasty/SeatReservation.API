﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeatReservation.Api.Database;

namespace SeatReservation.Api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SeatReservation.Api.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("genres");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<bool>("IsShutdown");

                    b.Property<string>("Logo");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("locations");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Banner")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Genres");

                    b.Property<bool>("IsArchived");

                    b.Property<bool>("IsFeatured");

                    b.Property<string>("Logo")
                        .IsRequired();

                    b.Property<int>("MovieLength");

                    b.Property<string>("Poster")
                        .IsRequired();

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Trailer")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("movies");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("permissions");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BookingDate");

                    b.Property<string>("Email");

                    b.Property<bool>("IsConfirmed");

                    b.Property<int>("ReservationStatus");

                    b.Property<int>("RoomId");

                    b.Property<int>("ScheduleSlotId");

                    b.Property<int>("SeatId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsOpen");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("RoomPlanId");

                    b.Property<int>("ScheduleId");

                    b.Property<int>("TechnologyId");

                    b.HasKey("Id");

                    b.ToTable("rooms");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.RoomAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LocationId");

                    b.Property<string>("RoomIds")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("room_assignments");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.RoomPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Columns");

                    b.Property<int>("Rows");

                    b.Property<string>("Seats");

                    b.HasKey("Id");

                    b.ToTable("room_plan");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.RoomTechnology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<double>("ExtraCharge");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("room_technologies");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MovieSchedule");

                    b.HasKey("Id");

                    b.ToTable("schedules");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.ScheduleSlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("End");

                    b.Property<int>("MovieId");

                    b.Property<string>("Reservations");

                    b.Property<int>("ScheduleId");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.ToTable("schedule_slots");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.SeatPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Column");

                    b.Property<int>("Rotation");

                    b.Property<int>("Row");

                    b.Property<int>("SeatTypeId");

                    b.HasKey("Id");

                    b.ToTable("seat_positions");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.SeatType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BasePrice");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("SeatCount");

                    b.Property<string>("SeatImage");

                    b.HasKey("Id");

                    b.ToTable("seat_types");
                });

            modelBuilder.Entity("SeatReservation.Api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Permissions")
                        .IsRequired();

                    b.Property<DateTime>("RegisterDate");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}
