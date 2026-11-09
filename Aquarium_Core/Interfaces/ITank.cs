using Aquarium_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Aquarium.Interfaces
{
    public interface ITank
    {
        Task<List<Tank>> GetAllTanksAsync(string? search = null, double? minVolume = null, string? sortOrder = null);

        Task<Tank?> GetTankByIdAsync(int id); //edit, delete

        Task<Tank?> GetTankDetailsAsync(int id); //details

        Task AddTankAsync(Tank tank);

        Task UpdateTankAsync(Tank tank);

        Task DeleteTankAsync(int id);
    }
}
