using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Configuration
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public ICollection<Location> Locations { get; set; }

        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("locations");
        }
    }
}
