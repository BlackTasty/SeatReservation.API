using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public ICollection<Genre> Genres { get; set; }

        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("genres");
        }
    }
}
