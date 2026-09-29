using Aquarium_Data;
using Aquarium_Data.Entities;
using Aquarium_Manager.ViewModels.Fish;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Aquarium_Manager.Controllers
{
    public class FishController : Controller
    {
        private readonly AquariumDB context;

        public FishController(AquariumDB context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var fish = await context.Fish
                .Include(f => f.Tank)
                .ToListAsync();

            var model = new List<FishIndexViewModel>();

            foreach (var fishItem in fish)
            {
                model.Add(new FishIndexViewModel
                {
                    Id = fishItem.Id,
                    Name = fishItem.Name,
                    Species = fishItem.Species,
                    Age = fishItem.Age,
                    TankId = fishItem.TankId
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var fish = await context.Fish
                .Include(f => f.Tank)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (fish == null)
            {
                return NotFound();
            }

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
            ViewBag.Tanks = new SelectList(
                await context.Tanks.ToListAsync(),
                "Id",
                "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FishCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fish = new Fish
                {
                    Name = model.Name,
                    Species = model.Species,
                    Age = model.Age,
                    TankId = model.TankId.Value
                };

                context.Fish.Add(fish);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Tanks = new SelectList(
                await context.Tanks.ToListAsync(),
                "Id",
                "Name");

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var fish = await context.Fish.FindAsync(id);

            if (fish == null)
            {
                return NotFound();
            }

            var model = new FishEditViewModel
            {
                Id = fish.Id,
                Name = fish.Name,
                Species = fish.Species,
                Age = fish.Age,
                TankId = fish.TankId
            };

            ViewBag.Tanks = new SelectList(
                await context.Tanks.ToListAsync(),
                "Id",
                "Name");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FishEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var fish = await context.Fish.FindAsync(id);

                if (fish == null)
                {
                    return NotFound();
                }

                fish.Name = model.Name;
                fish.Species = model.Species;
                fish.Age = model.Age;
                fish.TankId = model.TankId.Value;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Tanks = new SelectList(
                await context.Tanks.ToListAsync(),
                "Id",
                "Name");

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var fish = await context.Fish.FindAsync(id);

            if (fish == null)
            {
                return NotFound();
            }

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
            var fish = await context.Fish.FindAsync(id);

            if (fish != null)
            {
                context.Fish.Remove(fish);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
