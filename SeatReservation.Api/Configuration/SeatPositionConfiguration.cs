using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Configuration
{
    public class SeatPositionConfiguration : IEntityTypeConfiguration<SeatPosition>
    {
        public void Configure(EntityTypeBuilder<SeatPosition> builder)
        {
            builder.ToTable("seat_positions");
        }
    }
}
