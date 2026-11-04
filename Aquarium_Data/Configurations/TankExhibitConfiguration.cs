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
    public class TankExhibitConfiguration : IEntityTypeConfiguration<TanksExhibit>
    {
        public void Configure(EntityTypeBuilder<TanksExhibit> builder)
        {
            builder.HasData(
                new TanksExhibit
                {
                    TankId = 1,
                    ExhibitId = 1
                },
                new TanksExhibit
                {
                    TankId = 2,
                    ExhibitId = 2
                },
                new TanksExhibit
                {
                    TankId = 3,
                    ExhibitId = 3
                }
            );
        }
    }
}
