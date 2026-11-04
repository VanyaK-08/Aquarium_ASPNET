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
    public class ExhibitConfiguration : IEntityTypeConfiguration<Exhibit>
    {
        public void Configure(EntityTypeBuilder<Exhibit> builder)
        {
            builder.HasData(
                new Exhibit
                {
                    Id = 1,
                    Name = "Coral Reef Exhibit",
                    Description = "A vibrant display of coral reefs and tropical fish.",
                    IsDeleted = false
                },
                new Exhibit
                {
                    Id = 2,
                    Name = "Freshwater Exhibit",
                    Description = "A showcase of freshwater species from rivers and lakes.",
                    IsDeleted = false
                },
                new Exhibit
                {
                    Id = 3,
                    Name = "Deep Sea Exhibit",
                    Description = "An exploration of the mysterious deep sea creatures.",
                    IsDeleted = false
                }
            );
        }
    }
}
