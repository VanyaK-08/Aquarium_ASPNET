using Aquarium_Data;
using Aquarium_Data.Entities;
using Azure.Core;
using Core_Aquarium.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Aquarium.Services
{
    public class ExhibitService : IExhibit
    {
        private readonly AquariumDB context_new;

        public ExhibitService(AquariumDB context)
        {
            context_new = context;
        }

        public async Task AddExhibitAsync(Exhibit exhibit)
        {
            context_new.Exhibits.Add(exhibit);
            await context_new.SaveChangesAsync();
        }

        public async Task DeleteExhibitAsync(int id)
        {
            var exhibit = await context_new.Exhibits
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

            if (exhibit == null)
                return;

            exhibit.IsDeleted = true;

            await context_new.SaveChangesAsync();
        }

        public async Task<List<Exhibit>> GetAllExhibits()
        {
            return await context_new.Exhibits
                .Where(e => !e.IsDeleted)
                .Include(e => e.TanksExhibits)
                    .ThenInclude(te => te.Tank)
                .ToListAsync();
        }

        public async Task<Exhibit?> GetExhibitByIdAsync(int id)
        {
            return await context_new.Exhibits
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<Exhibit?> GetExhibitDetailsAsync(int id)
        {
            return await context_new.Exhibits
                .Include(e => e.TanksExhibits)
                    .ThenInclude(te => te.Tank)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task UpdateExhibitAsync(Exhibit exhibit)
        {
            await context_new.SaveChangesAsync();
        }
    }
}
