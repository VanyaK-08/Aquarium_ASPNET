using Aquarium_Data.Configurations;
using Aquarium_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquarium_Data
{
    public class AquariumDB :DbContext
    {
        public AquariumDB(DbContextOptions<AquariumDB> options) : base(options)
        {

        }

        public DbSet<Tank> Tanks { get; set; }
        public DbSet<Fish> Fish { get; set; }
        public DbSet<Exhibit> Exhibits { get; set; }
        public DbSet<TanksExhibit> TanksExhibits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TankConfiguration());
            modelBuilder.ApplyConfiguration(new FishConfiguration());
            modelBuilder.ApplyConfiguration(new ExhibitConfiguration());
            modelBuilder.ApplyConfiguration(new TankExhibitConfiguration());
        }
    }
}
