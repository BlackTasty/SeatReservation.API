using Microsoft.EntityFrameworkCore;
using SeatReservation.Api.Configuration;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }

        //public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomPlan> RoomPlans { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<ScheduleSlot> ScheduleSlots { get; set; }

        public DbSet<SeatType> SeatTypes { get; set; }

        public DbSet<SeatPosition> SeatPositions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Location> Locations{ get; set; }

        public DbSet<RoomAssignment> RoomAssignments { get; set; }

        public DbSet<RoomTechnology> RoomTechnologies{ get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Studio> Studios{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=seat_reservation;User=root;Password=root;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            //modelBuilder.ApplyConfiguration(new MovieGenreConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            //modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new RoomPlanConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleSlotConfiguration());
            modelBuilder.ApplyConfiguration(new SeatTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SeatPositionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new RoomAssignmentConfiguration());
            modelBuilder.ApplyConfiguration(new RoomTechnologyConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new StudioConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
