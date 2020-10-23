using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Configuration
{
    public class SeatTypeConfiguration : IEntityTypeConfiguration<SeatType>
    {
        public ICollection<SeatType> SeatTypes { get; set; }

        public void Configure(EntityTypeBuilder<SeatType> builder)
        {
            builder.ToTable("seat_types");
        }
    }
}
