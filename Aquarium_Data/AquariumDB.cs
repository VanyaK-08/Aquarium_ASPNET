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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fish>()
                .HasOne(f => f.Tank)
                .WithMany(t => t.Fish)
                .HasForeignKey(f => f.TankId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
