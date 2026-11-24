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

        public async Task<List<Fish>> GetAllFishAsync(string? search = null, string? species = null, string? sortOrder = null)
        {
            var query = context_new.Fish
                .Include(f => f.Tank)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(f =>
                    f.Name.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(species))
            {
                query = query.Where(f =>
                    f.Species.Contains(species));
            }

            switch (sortOrder)
            {
                case "name":
                    query = query.OrderBy(f => f.Name);
                    break;

                case "species":
                    query = query.OrderBy(f => f.Species);
                    break;

                case "age_asc":
                    query = query.OrderBy(f => f.Age);
                    break;

                case "age_desc":
                    query = query.OrderByDescending(f => f.Age);
                    break;

                default:
                    query = query.OrderBy(f => f.Id);
                    break;
            }

            return await query.ToListAsync();
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
