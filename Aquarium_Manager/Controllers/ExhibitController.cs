using Aquarium_Data;
using Aquarium_Data.Entities;
using Aquarium_Manager.ViewModels.Exhibit;
using Core_Aquarium.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Aquarium_Manager.Controllers
{
    public class ExhibitController : Controller
    {
        private readonly IExhibit exhibitService;
        private readonly ITank tankService;

        public ExhibitController(IExhibit exhibitService, ITank tankService)
        {
            this.exhibitService = exhibitService;
            this.tankService = tankService;
        }

        public async Task<IActionResult> Index()
        {
            var exhibits = await exhibitService.GetAllExhibits();

            var model = exhibits.Select(e => new ExhibitIndexViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var exhibit = await exhibitService.GetExhibitDetailsAsync(id);

            if (exhibit == null)
                return NotFound();

            var model = new ExhibitDetailsViewModel
            {
                Id = exhibit.Id,
                Name = exhibit.Name,
                Description = exhibit.Description,
                TankNames = exhibit.TanksExhibits
                    .Where(te => te.Tank != null && !te.Tank.IsDeleted)
                    .Select(te => te.Tank.Name)
                    .ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            await LoadTanksAsync();

            return View(new ExhibitCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExhibitCreateViewModel model)
        {
            var selectedTankIds = (model.SelectedTankIds ?? new List<int>())
                .Distinct()
                .ToList();

            if (!ModelState.IsValid)
            {
                await LoadTanksAsync();
                return View(model);
            }

            var tanks = await tankService.GetAllTanksAsync();
            var validTankIds = tanks.Select(t => t.Id).ToHashSet();

            if (!selectedTankIds.All(id => validTankIds.Contains(id)))
            {
                await LoadTanksAsync();
                return View(model);
            }

            var exhibit = new Exhibit
            {
                Name = model.Name,
                Description = model.Description,
                TanksExhibits = selectedTankIds.Select(tankId =>
                    new TanksExhibit
                    {
                        TankId = tankId
                    }).ToList()
            };

            await exhibitService.AddExhibitAsync(exhibit);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var exhibit = await exhibitService.GetExhibitDetailsAsync(id);

            if (exhibit == null)
                return NotFound();

            var model = new ExhibitEditWiewModel
            {
                Id = exhibit.Id,
                Name = exhibit.Name,
                Description = exhibit.Description,
                SelectedTankIds = exhibit.TanksExhibits
                    .Where(te => te.Tank != null && !te.Tank.IsDeleted)
                    .Select(te => te.TankId)
                    .ToList()
            };

            await LoadTanksAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ExhibitEditWiewModel model)
        {
            var selectedTankIds = (model.SelectedTankIds ?? new List<int>())
                .Distinct()
                .ToList();

            if (!ModelState.IsValid)
            {
                await LoadTanksAsync();
                return View(model);
            }

            var tanks = await tankService.GetAllTanksAsync();
            var validTankIds = tanks.Select(t => t.Id).ToHashSet();

            if (!selectedTankIds.All(tankId => validTankIds.Contains(tankId)))
            {
                await LoadTanksAsync();
                return View(model);
            }

            var exhibit = await exhibitService.GetExhibitDetailsAsync(id);

            exhibit.Name = model.Name;
            exhibit.Description = model.Description;

            exhibit.TanksExhibits.Clear();

            foreach (var tankId in selectedTankIds)
            {
                exhibit.TanksExhibits.Add(new TanksExhibit
                {
                    ExhibitId = exhibit.Id,
                    TankId = tankId
                });
            }

            await exhibitService.UpdateExhibitAsync(exhibit);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var exhibit = await exhibitService.GetExhibitByIdAsync(id);

            if (exhibit == null)
                return NotFound();

            var model = new ExhibitDeleteViewModel
            {
                Id = exhibit.Id,
                Name = exhibit.Name
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exhibit = await exhibitService.GetExhibitByIdAsync(id);

            if (exhibit == null)
                return NotFound();

            await exhibitService.DeleteExhibitAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadTanksAsync()
        {
            var tanks = await tankService.GetAllTanksAsync();

            ViewBag.Tanks = new MultiSelectList(
                tanks,
                "Id",
                "Name"
            );
        }
    }
}
