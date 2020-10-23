using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Configuration
{
    public class ReservationScheduleSlotConfiguration : IEntityTypeConfiguration<ReservationScheduleSlot>
    {
        public void Configure(EntityTypeBuilder<ReservationScheduleSlot> builder)
        {
            builder.ToTable("seat_position");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RoomId).IsRequired();
            builder.Property(x => x.SeatId).IsRequired();
            builder.Property(x => x.ReservationStatus).IsRequired().HasDefaultValue(ReservationStatus.Free);

            builder.HasOne(x => x.ScheduleSlot)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.ScheduleSlotId);
        }
    }
}
