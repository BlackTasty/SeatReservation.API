using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Configuration
{
    public class RoomAssignmentConfiguration : IEntityTypeConfiguration<RoomAssignment>
    {
        public ICollection<RoomAssignment> RoomAssignments { get; set; }

        public void Configure(EntityTypeBuilder<RoomAssignment> builder)
        {
            builder.ToTable("room_assignments");
        }
    }
}
