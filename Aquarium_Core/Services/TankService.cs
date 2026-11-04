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

        public async Task<List<Tank>> GetAllTanksAsync()
        {
            return await context_new.Tanks
                .Where(t => !t.IsDeleted)
                .Include(t => t.Fish)
                .Include(t => t.TanksExhibits)
                    .ThenInclude(te => te.Exhibit)
                .ToListAsync();
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
