using Aquarium_Data;
using Aquarium_Data.Entities;
using Aquarium_Manager.ViewModels.Fish;
using Core_Aquarium.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Aquarium_Manager.Controllers
{
    public class FishController : Controller
    {
        private readonly IFish fishService;
        private readonly ITank tankService;

        public FishController(IFish fishService, ITank tankService)
        {
            this.fishService = fishService;
            this.tankService = tankService;
        }

        public async Task<IActionResult> Index()
        {
            var fishList = await fishService.GetAllFishAsync();

            var model = fishList.Select(f => new FishIndexViewModel
            {
                Id = f.Id,
                Name = f.Name,
                Species = f.Species,
                Age = f.Age,
                TankId = f.TankId
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var fish = await fishService.GetFishDetailsAsync(id);

            if (fish == null)
                return NotFound();

            var model = new FishDetailsViewModel
            {
                Id = fish.Id,
                Name = fish.Name,
                Species = fish.Species,
                Age = fish.Age,
                TankId = fish.TankId
            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            await LoadTanksAsync();

            return View(new FishCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(FishCreateViewModel model)
        {
            await LoadTanksAsync();

            if (!ModelState.IsValid)
                return View(model);

            var tanks = await tankService.GetAllTanksAsync();

            if (!tanks.Any(t => t.Id == model.TankId))
            {

                return View(model);
            }

            var fish = new Fish
            {
                Name = model.Name,
                Species = model.Species,
                Age = model.Age,
                TankId = (int)model.TankId
            };

            await fishService.AddFishAsync(fish);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var fish = await fishService.GetFishByIdAsync(id);

            if (fish == null)
                return NotFound();

            var model = new FishEditViewModel
            {
                Id = fish.Id,
                Name = fish.Name,
                Species = fish.Species,
                Age = fish.Age,
                TankId = fish.TankId
            };

            await LoadTanksAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FishEditViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            await LoadTanksAsync();

            if (!ModelState.IsValid)
                return View(model);

            var tanks = await tankService.GetAllTanksAsync();

            if (!tanks.Any(t => t.Id == model.TankId))
            {
                return View(model);
            }

            var fish = await fishService.GetFishByIdAsync(id);

            if (fish == null)
                return NotFound();

            fish.Name = model.Name;
            fish.Species = model.Species;
            fish.Age = model.Age;
            fish.TankId = (int)model.TankId;

            await fishService.UpdateFishAsync(fish);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var fish = await fishService.GetFishByIdAsync(id);

            if (fish == null)
                return NotFound();

            var model = new FishDeleteViewModel
            {
                Id = fish.Id,
                Name = fish.Name,
                Species = fish.Species
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fish = await fishService.GetFishByIdAsync(id);

            if (fish == null)
                return NotFound();

            await fishService.DeleteFishAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadTanksAsync()
        {
            var tanks = await tankService.GetAllTanksAsync();

            ViewBag.Tanks = new SelectList(
                tanks,
                "Id",
                "Name"
            );
        }
    }
}
