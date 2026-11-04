using Aquarium_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Aquarium.Interfaces
{
    public interface IFish
    {
        Task<List<Fish>> GetAllFishAsync();

        Task<Fish?> GetFishByIdAsync(int id);

        Task<Fish?> GetFishDetailsAsync(int id);

        Task AddFishAsync(Fish fish);

        Task UpdateFishAsync(Fish fish);

        Task DeleteFishAsync(int id);
    }
}
