using Aquarium_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquarium_Data.Configurations
{
    public class TankConfiguration : IEntityTypeConfiguration<Tank>
    {
        public void Configure(EntityTypeBuilder<Tank> builder)
        {
            builder.HasData(
                new Tank
                {
                    Id = 2,
                    Name = "Tropical Tank",
                    Volume = 100,
                    Location = "Main Hall",
                    IsDeleted = false
                },
                new Tank
                {
                    Id = 3,
                    Name = "Freshwater Tank",
                    Volume = 150,
                    Location = "Freshwater Section",
                    IsDeleted = false
                },
                new Tank
                {
                    Id = 4,
                    Name = "Deep Sea Tank",
                    Volume = 200,
                    Location = "Deep Sea Section",
                    IsDeleted = false
                }
            );
        }
    }
}
