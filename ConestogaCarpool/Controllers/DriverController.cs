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
    public class DriverController : Controller
    {
        private readonly ConestogaCarpoolContext _context;
        private IDriverLogic _driverLogic;

        public DriverController(ConestogaCarpoolContext context, IDriverLogic driverLogic)
        {
            _context = context;
            _driverLogic = driverLogic;
        }

        // GET: Driver
        public async Task<IActionResult> Index(int? UserId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            if (UserId != null)
            {
                HttpContext.Session.SetInt32("UserId", Convert.ToInt32(UserId));
            }
            else if (UserId == null)
            {
                if (HttpContext.Session.GetInt32("UserId") != null)
                {
                    UserId = HttpContext.Session.GetInt32("UserId");
                }
            }

            // Save userId value to viewbag
            ViewBag.UserId = UserId;

            if (!_driverLogic.IsUserDriver(UserId))
            {
                return RedirectToAction(nameof(Create));
            }

            var driver = _driverLogic.GetDrivers(UserId);

            return View(await driver);
        }

        // GET: Driver/Details/5
        public async Task<IActionResult> Details(int? DriverId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (DriverId == null)
            {
                return NotFound();
            }

            var driver = await _driverLogic.GetSingleDriver(DriverId);

            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // GET: Driver/Create
        public IActionResult Create(int? UserId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            ViewData["LicenceClassId"] = new SelectList(_context.LicenceClass, "LicenceClassId", "LicenceClass1");

            return View();
        }

        // POST: Driver/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DriverId,UserId,LicenceClassId,Experience")] Driver driver)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            driver.UserId = ViewBag.UserId;

            if (ModelState.IsValid)
            {
                _driverLogic.CreateDriver(driver);
                await _driverLogic.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LicenceClassId"] = new SelectList(_context.LicenceClass, "LicenceClassId", "LicenceClass1", driver.LicenceClassId);

            return View(driver);
        }

        // GET: Driver/Edit/5
        public async Task<IActionResult> Edit(int? DriverId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (DriverId == null)
            {
                return NotFound();
            }

            var driver = await _driverLogic.GetDriver(DriverId);
            if (driver == null)
            {
                return NotFound();
            }

            ViewData["LicenceClassId"] = new SelectList(_context.LicenceClass, "LicenceClassId", "LicenceClass1", driver.LicenceClassId);

            return View(driver);
        }

        // POST: Driver/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int DriverId, [Bind("DriverId,UserId,LicenceClassId,Experience")] Driver driver)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (DriverId != driver.DriverId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    driver.UserId = ViewBag.UserId;
                    _driverLogic.UpdateDriver(driver);
                    await _driverLogic.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.DriverId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LicenceClassId"] = new SelectList(_context.LicenceClass, "LicenceClassId", "LicenceClass1", driver.LicenceClassId);

            return View(driver);
        }

        // GET: Driver/Delete/5
        public async Task<IActionResult> Delete(int? DriverId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (DriverId == null)
            {
                return NotFound();
            }

            var driver = await _driverLogic.GetSingleDriver(DriverId);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Driver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int DriverId)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            _driverLogic.DeleteDriver(DriverId);
            await _driverLogic.Save();

            return RedirectToAction("Index", "User");
        }

        private bool DriverExists(int id)
        {
            return _driverLogic.DriverExists(id);
        }
    }
}
