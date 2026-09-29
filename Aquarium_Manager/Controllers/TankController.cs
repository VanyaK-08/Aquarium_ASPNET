using Aquarium_Data;
using Aquarium_Data.Entities;
using Aquarium_Manager.ViewModels.Tank;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aquarium_Manager.Controllers
{
    public class TankController : Controller
    {
        private readonly AquariumDB context;

        public TankController(AquariumDB context)
        {
            this.context = context;
        }


        public async Task<IActionResult> Index()
        {
            var tanks = await context.Tanks
                .ToListAsync();

            var model = new List<TankIndexViewModel>();

            foreach (var tank in tanks)
            {
                model.Add(new TankIndexViewModel
                {
                    Id = tank.Id,
                    Name = tank.Name,
                    Volume = tank.Volume,
                    Location = tank.Location
                });
            }

            return View(model);
        }


        public async Task<IActionResult> Details(int id)
        {
            var tank = await context.Tanks.FindAsync(id);

            if (tank == null)
            {
                return NotFound();
            }

            var model = new TankDetailsViewModel
            {
                Id = tank.Id,
                Name = tank.Name,
                Volume = tank.Volume,
                Location = tank.Location
            };

            return View(model);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(TankCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tank = new Tank
                {
                    Name = model.Name,
                    Volume = model.Volume,
                    Location = model.Location
                };

                context.Tanks.Add(tank);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var tank = await context.Tanks.FindAsync(id);

            if (tank == null)
            {
                return NotFound();
            }

            var model = new TankEditViewModel
            {
                Id = tank.Id,
                Name = tank.Name,
                Volume = tank.Volume,
                Location = tank.Location
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, TankEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var tank = await context.Tanks.FindAsync(id);

                if (tank == null)
                {
                    return NotFound();
                }

                tank.Name = model.Name;
                tank.Volume = model.Volume;
                tank.Location = model.Location;

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var tank = await context.Tanks.FindAsync(id);

            if (tank == null)
            {
                return NotFound();
            }

            var model = new TankDeleteViewModel
            {
                Id = tank.Id,
                Name = tank.Name,
            };

            return View(model);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tank = await context.Tanks.FindAsync(id);

            if (tank != null)
            {
                context.Tanks.Remove(tank);

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
