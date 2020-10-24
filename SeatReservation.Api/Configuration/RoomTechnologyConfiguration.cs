using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Configuration
{
    public class RoomTechnologyConfiguration : IEntityTypeConfiguration<RoomTechnology>
    {
        public ICollection<RoomTechnology> RoomTechnologies { get; set; }

        public void Configure(EntityTypeBuilder<RoomTechnology> builder)
        {
            builder.ToTable("room_technologies");
        }
    }
}
