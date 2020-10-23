using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeatReservation.Api.Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public ICollection<Permission> Permissions { get; set; }

        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permissions");
        }
    }
}
