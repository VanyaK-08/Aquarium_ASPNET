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
    public class TankService : ITank
    {
        private readonly AquariumDB context_new;

        public TankService(AquariumDB context)
        {
            context_new = context;
        }

        public async Task AddTankAsync(Tank tank)
        {
            context_new.Tanks.Add(tank);

            await context_new.SaveChangesAsync();
        }

        public async Task DeleteTankAsync(int id)
        {
            var tank = await context_new.Tanks
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);

            if (tank == null)
                return;

            tank.IsDeleted = true;

            await context_new.SaveChangesAsync();
        }

        public async Task<List<Tank>> GetAllTanksAsync(string? search = null, double? minVolume = null, string? sortOrder = null)
        {
            var query = context_new.Tanks.Where(t => !t.IsDeleted);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Name.Contains(search));
            }
            if (minVolume.HasValue)
            {
                query = query.Where(h=> h.Volume >= minVolume.Value);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderBy(h => h.Name);
                    break;
                case "volume_asc":
                    query = query.OrderBy(h => h.Volume);
                    break;
                case "volume_desc":
                    query = query.OrderByDescending(h => h.Volume);
                    break;
                default:
                    query = query.OrderBy(h => h.Name);
                    break;
            }
            return await query.ToListAsync();
        }

        public async Task<Tank?> GetTankByIdAsync(int id)
        {
            return await context_new.Tanks
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task<Tank?> GetTankDetailsAsync(int id)
        {
            return await context_new.Tanks
                .Include(t => t.Fish)
                .Include(t => t.TanksExhibits)
                    .ThenInclude(te => te.Exhibit)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task UpdateTankAsync(Tank tank)
        {
            await context_new.SaveChangesAsync();
        }
    }
}
