using Aquarium_Data;
using Aquarium_Data.Entities;
using Aquarium_Manager.ViewModels.Tank;
using Core_Aquarium.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Aquarium_Manager.Controllers
{
    public class TankController : Controller
    {
        private readonly ITank tankService;
        private readonly IExhibit exhibitService;

        public TankController(ITank tankService, IExhibit exhibitService)
        {
            this.tankService = tankService;
            this.exhibitService = exhibitService;
        }

        public async Task<IActionResult> Index()
        {
            var tanks = await tankService.GetAllTanksAsync();

            var model = tanks.Select(t => new TankIndexViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Volume = t.Volume,
                Location = t.Location
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var tank = await tankService.GetTankDetailsAsync(id);

            if (tank == null)
                return NotFound();

            var model = new TankDetailsViewModel
            {
                Id = tank.Id,
                Name = tank.Name,
                Volume = tank.Volume,
                Location = tank.Location,
                ExhibitNames = tank.TanksExhibits
                    .Where(te => te.Exhibit != null && !te.Exhibit.IsDeleted)
                    .Select(te => te.Exhibit.Name)
                    .ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            await LoadExhibitsAsync();

            return View(new TankCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TankCreateViewModel model)
        {
            var selectedExhibitIds = model.SelectedExhibitIds.Distinct().ToList();

            if (!ModelState.IsValid)
            {
                await LoadExhibitsAsync();
                return View(model);
            }

            var exhibits = await exhibitService.GetAllExhibits();
            var validExhibitIds = exhibits.Select(e => e.Id).ToHashSet();

            if (!selectedExhibitIds.All(id => validExhibitIds.Contains(id)))
            {
                await LoadExhibitsAsync();
                return View(model);
            }

            var tank = new Tank
            {
                Name = model.Name,
                Volume = model.Volume,
                Location = model.Location,
                TanksExhibits = selectedExhibitIds.Select(exhibitId =>
                    new TanksExhibit
                    {
                        ExhibitId = exhibitId
                    }).ToList()
            };

            await tankService.AddTankAsync(tank);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tank = await tankService.GetTankDetailsAsync(id);

            if (tank == null)
                return NotFound();

            var model = new TankEditViewModel
            {
                Id = tank.Id,
                Name = tank.Name,
                Volume = tank.Volume,
                Location = tank.Location,
                SelectedExhibitIds = tank.TanksExhibits
                    .Where(te => te.Exhibit != null && !te.Exhibit.IsDeleted)
                    .Select(te => te.ExhibitId)
                    .ToList()
            };

            await LoadExhibitsAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TankEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadExhibitsAsync();
                return View(model);
            }

            var tank = await tankService.GetTankDetailsAsync(id);

            if (tank == null)
                return NotFound();

            var selectedExhibitIds = model.SelectedExhibitIds
                .Distinct()
                .ToList();

            var allExhibits = await exhibitService.GetAllExhibits();

            var validExhibitIds = allExhibits
                .Select(e => e.Id)
                .ToList();

            if (selectedExhibitIds.Any(id => !validExhibitIds.Contains(id)))
            {
                await LoadExhibitsAsync();
                return View(model);
            }

            tank.Name = model.Name;
            tank.Volume = model.Volume;
            tank.Location = model.Location;

            tank.TanksExhibits.Clear();

            foreach (var exhibitId in selectedExhibitIds)
            {
                tank.TanksExhibits.Add(new TanksExhibit
                {
                    TankId = tank.Id,
                    ExhibitId = exhibitId
                });
            }

            await tankService.UpdateTankAsync(tank);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var tank = await tankService.GetTankByIdAsync(id);

            if (tank == null)
                return NotFound();

            var model = new TankDeleteViewModel
            {
                Id = tank.Id,
                Name = tank.Name
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tank = await tankService.GetTankByIdAsync(id);

            if (tank == null)
                return NotFound();

            await tankService.DeleteTankAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadExhibitsAsync()
        {
            var exhibits = await exhibitService.GetAllExhibits();

            ViewBag.Exhibits = new MultiSelectList(
                exhibits,
                "Id",
                "Name"
            );
        }
    }
}
