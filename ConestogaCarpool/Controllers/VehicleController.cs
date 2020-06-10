using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConestogaCarpool.Models;
using Microsoft.AspNetCore.Http;
using ConestogaCarpool.BusinessLogic;

namespace ConestogaCarpool.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ConestogaCarpoolContext _context;
        private IVehicleLogic _vehicleLogic;

        public VehicleController(ConestogaCarpoolContext context, IVehicleLogic vehicleLogic)
        {
            _context = context;
            _vehicleLogic = vehicleLogic;
        }

        // GET: Vehicle
        public async Task<IActionResult> SelectVehicle(int? UserId, int? DriverId)
        {
            HttpContext.Session.SetInt32("DriverId", Convert.ToInt32(DriverId));

            if (UserId == null && HttpContext.Session.GetInt32("UserId") != null)
            {
                UserId = HttpContext.Session.GetInt32("UserId");
                DriverId = HttpContext.Session.GetInt32("DriverId");
            }

            // Save values to viewbag
            ViewBag.UserId = UserId;
            ViewBag.DriverId = DriverId;

            // Get list of vehicles
            var vehiclesOwned = await _vehicleLogic.GetVehiclesOwned(UserId);

            if (vehiclesOwned.Count == 0)
            {
                // if count = 0, register vehicle
                return RedirectToAction(nameof(CreateForPost));
            }

            else
            {
                return View(vehiclesOwned);
            }
        }

        // GET: Vehicle
        public async Task<IActionResult> ListVehicles(int? UserId)
        {
            TempData["Message"] = null;

            ViewBag.Username = HttpContext.Session.GetString("Username");
            UserId = HttpContext.Session.GetInt32("UserId");

            // Save userId value to viewbag
            ViewBag.UserId = UserId;

            // Get list of vehicles
            var vehiclesOwned = await _vehicleLogic.GetVehiclesOwned((int)UserId);

            // Check count of vehicles owned by user
            if (vehiclesOwned.Count == 0)
            {
                TempData["Message"] = "No Vehicles to Show";
            }

            return View(vehiclesOwned);
        }

        // GET: Vehicle/Details/5
        public async Task<IActionResult> Details(int? VehicleId)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (VehicleId == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleLogic.GetSingleVehicle(VehicleId);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicle/Create
        public IActionResult Create()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            return View();
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,UserId,Make,Model,Year,Colour,Plate")] Vehicle vehicle)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            vehicle.UserId = ViewBag.UserId;

            if (ModelState.IsValid)
            {
                _vehicleLogic.CreateVehicle(vehicle);
                await _vehicleLogic.Save();

                return RedirectToAction(nameof(ListVehicles));
            }

            return View(vehicle);
        }

        // GET: Vehicle/Create
        public IActionResult CreateForPost()
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            return View();
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateForPost([Bind("VehicleId,UserId,Make,Model,Year,Colour,Plate")] Vehicle vehicle)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            vehicle.UserId = ViewBag.UserId;

            if (ModelState.IsValid)
            {
                _vehicleLogic.CreateVehicle(vehicle);
                await _vehicleLogic.Save();

                return RedirectToAction(nameof(SelectVehicle));
            }

            return View(vehicle);
        }

        // GET: Vehicle/Edit/5
        public async Task<IActionResult> Edit(int? VehicleId)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (VehicleId == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleLogic.GetSingleVehicle(VehicleId);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int VehicleId, [Bind("VehicleId,UserId,Make,Model,Year,Colour,Plate")] Vehicle vehicle)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            vehicle.UserId = ViewBag.UserId;

            if (VehicleId != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vehicleLogic.UpdateVehicle(vehicle);

                    await _vehicleLogic.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListVehicles));
            }

            return View(vehicle);
        }

        // GET: Vehicle/Delete/5
        public async Task<IActionResult> Delete(int? VehicleId)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (VehicleId == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleLogic.GetSingleVehicle(VehicleId);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int VehicleId)
        {
            _vehicleLogic.DeleteVehicle(VehicleId);
            await _vehicleLogic.Save();

            return RedirectToAction(nameof(ListVehicles));
        }

        private bool VehicleExists(int id)
        {
            return _vehicleLogic.VehicleExists(id);
        }
    }
}
