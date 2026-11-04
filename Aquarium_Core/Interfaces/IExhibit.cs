using Aquarium_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Aquarium.Interfaces
{
    public interface IExhibit
    {
        Task<List<Exhibit>> GetAllExhibits();
        Task<Exhibit> GetExhibitByIdAsync(int id); //edit, delete
        Task<Exhibit> GetExhibitDetailsAsync(int id); //details
        Task AddExhibitAsync(Exhibit exhibit);
        Task UpdateExhibitAsync(Exhibit exhibit);
        Task DeleteExhibitAsync(int id);
    }
}
