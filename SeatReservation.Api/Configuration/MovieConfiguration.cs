using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public ICollection<Movie> Movies{ get; set; }

        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("movies");
        }
    }
}
