using Aquarium_Data;
using Aquarium_Data.Entities;
using Core_Aquarium.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Aquarium.Services
{
    public class FishService : IFish
    {
        private readonly AquariumDB context_new;

        public FishService(AquariumDB context)
        {
            context_new = context;
        }

        public async Task AddFishAsync(Fish fish)
        {
            var tankExists = await context_new.Tanks
                .AnyAsync(t => t.Id == fish.TankId && !t.IsDeleted);

            context_new.Fish.Add(fish);

            await context_new.SaveChangesAsync();
        }

        public async Task DeleteFishAsync(int id)
        {
            var fish = await context_new.Fish
                .FirstOrDefaultAsync(f => f.Id == id);

            if (fish == null)
                return;

            context_new.Fish.Remove(fish);

            await context_new.SaveChangesAsync();
        }

        public async Task<List<Fish>> GetAllFishAsync()
        {
            return await context_new.Fish
                .Include(f => f.Tank)
                .Where(f => !f.Tank!.IsDeleted)
                .ToListAsync();
        }

        public async Task<Fish?> GetFishByIdAsync(int id)
        {
            return await context_new.Fish
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fish?> GetFishDetailsAsync(int id)
        {
            return await context_new.Fish
                .Include(f => f.Tank)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task UpdateFishAsync(Fish fish)
        {
            await context_new.SaveChangesAsync();
        }
    }
}
