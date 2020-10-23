using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Configuration
{
    public class RoomPlanConfiguration : IEntityTypeConfiguration<RoomPlan>
    {
        public void Configure(EntityTypeBuilder<RoomPlan> builder)
        {
            builder.ToTable("room_plan");
            builder.Property(x => x.Columns).IsRequired();
            builder.Property(x => x.Rows).IsRequired();

            /*builder.HasMany(x => x.Seats)
                .WithOne(x => x.RoomPlan)
                .HasForeignKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);*/
        }
    }
}
