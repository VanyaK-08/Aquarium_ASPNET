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
    public class FishConfiguration : IEntityTypeConfiguration<Fish>
    {
        public void Configure(EntityTypeBuilder<Fish> builder)
        {
            builder.HasData(
                new Fish
                {
                    Id = 2,
                    Name = "Clownfish",
                    Species = "Amphiprioninae",
                    Age = 2,
                    TankId = 1
                },
                new Fish
                {
                    Id = 3,
                    Name = "Guppy",
                    Species = "Poecilia reticulata",
                    Age = 1,
                    TankId = 2
                },
                new Fish
                {
                    Id = 4,
                    Name = "Angelfish",
                    Species = "Pterophyllum scalare",
                    Age = 3,
                    TankId = 1
                }
            );
        }

    }
}
