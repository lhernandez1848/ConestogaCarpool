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
    public class RideController : Controller
    {
        private readonly ConestogaCarpoolContext _context;
        private IRideLogic _rideLogic;

        public RideController(ConestogaCarpoolContext context, IRideLogic rideLogic)
        {
            _context = context;
            _rideLogic = rideLogic;
        }

        // GET: Ride
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            return View();
        }

        public async Task<IActionResult> DriverIndex(int? DriverId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            TempData["Message"] = null;
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.DriverId = DriverId;

            List<Ride> driverHistory = await _rideLogic.GetDriverRides(DriverId);
            HttpContext.Session.SetInt32("DriverId", Convert.ToInt32(DriverId));

            if (driverHistory.Count == 0)
            {
                TempData["Message"] = "You have no rides";
            }

            return View(driverHistory);
        }

        public async Task<IActionResult> PassengerIndex(int? PassengerId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            if (HttpContext.Session.GetInt32("PassengerId") == null || HttpContext.Session.GetInt32("PassengerId") == 0)
            {
                HttpContext.Session.SetInt32("PassengerId", Convert.ToInt32(PassengerId));
            }
            else if (PassengerId == null)
            {
                PassengerId = HttpContext.Session.GetInt32("PassengerId");
            }

            TempData["Message"] = null;
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.PassengerId = HttpContext.Session.GetInt32("PassengerId");

            List<Ride> passengerHistory = await _rideLogic.GetPassengerRides(PassengerId);

            if (passengerHistory.Count == 0)
            {
                TempData["Message"] = "You have no rides";
            }

            return View(passengerHistory);
        }

        // GET: Ride/Details/5
        public async Task<IActionResult> Details(int? RideId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (RideId == null)
            {
                return NotFound();
            }

            Ride ride = await _rideLogic.GetSingleRide(RideId);

            if (ride == null)
            {
                return NotFound();
            }

            return View(ride);
        }

        // GET: Ride/Create
        public IActionResult Create(int? postId, int? requestId)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.RequestId = requestId;
            ViewBag.PostId = postId;

            //ViewData["PostId"] = new SelectList(_context.Post, "PostId", "Destination");
            //ViewData["RequestId"] = new SelectList(_context.Request, "RequestId", "RequestId");
            //ViewData["RideStatusId"] = new SelectList(_context.RideStatus, "RideStatusId", "RideStatusDescription");
            return View();
        }

        // POST: Ride/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RideId,RideStatusId,PostId,RequestId")] Ride ride)
        {
            if (ModelState.IsValid)
            {
                _rideLogic.CreateRide(ride);
                await _rideLogic.Save();

                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index), "User");
            }
            ViewData["PostId"] = new SelectList(_context.Post, "PostId", "Destination", ride.PostId);
            ViewData["RequestId"] = new SelectList(_context.Request, "RequestId", "RequestId", ride.RequestId);
            ViewData["RideStatusId"] = new SelectList(_context.RideStatus, "RideStatusId", "RideStatusDescription", ride.RideStatusId);
            return View(ride);
        }

        // GET: Ride/Edit/5
        public async Task<IActionResult> Edit(int? RideId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (RideId == null)
            {
                return NotFound();
            }

            Ride ride = await _rideLogic.GetSingleRide(RideId);

            if (ride == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Post, "PostId", "Destination", ride.PostId);
            ViewData["RequestId"] = new SelectList(_context.Request, "RequestId", "RequestId", ride.RequestId);
            ViewData["RideStatusId"] = new SelectList(_context.RideStatus, "RideStatusId", "RideStatusDescription", ride.RideStatusId);
            return View(ride);
        }

        // POST: Ride/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int RideId, [Bind("RideId,RideStatusId,PostId,RequestId")] Ride ride)
        {
            if (RideId != ride.RideId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _rideLogic.UpdateRide(ride);
                    await _rideLogic.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RideExists(ride.RideId))
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
            ViewData["PostId"] = new SelectList(_context.Post, "PostId", "Destination", ride.PostId);
            ViewData["RequestId"] = new SelectList(_context.Request, "RequestId", "RequestId", ride.RequestId);
            ViewData["RideStatusId"] = new SelectList(_context.RideStatus, "RideStatusId", "RideStatusDescription", ride.RideStatusId);
            return View(ride);
        }

        // GET: Ride/Delete/5
        public async Task<IActionResult> Delete(int? RideId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (RideId == null)
            {
                return NotFound();
            }

            Ride ride = await _rideLogic.GetSingleRide(RideId);

            if (ride == null)
            {
                return NotFound();
            }

            return View(ride);
        }

        // POST: Ride/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int RideId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            _rideLogic.DeleteRide(RideId);
            await _rideLogic.Save();
            return RedirectToAction(nameof(Index));
        }

        //GET: Ride/Complete/5
        public async Task<IActionResult> Complete(int? DriverId, int? RideId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            // Save userId to viewbag from session
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (RideId == null)
            {
                return NotFound();
            }

            Ride ride = await _rideLogic.GetSingleRide(RideId);

            if (ride == null)
            {
                return NotFound();
            }

            // Save to viewbag
            ViewBag.PostId = ride.PostId;
            ViewBag.RequestId = ride.RequestId;
            ViewBag.DriverId = DriverId;

            return View(ride);
        }

        // POST: Ride/Complete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int RideId, [Bind("RideId,RideStatusId,PostId,RequestId")] Ride ride,
            int? driverId)
        {
            if (RideId != ride.RideId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _rideLogic.UpdateRide(ride);
                    await _rideLogic.Save();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!RideExists(ride.RideId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // redirect to driverIndex
                //return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(DriverIndex), new { DriverId = driverId });
            }

            return View(ride);
        }

        /*public async Task<IActionResult> Yes(int? RideId, int? UserId)
        {
            Ride ride = await _rideLogic.GetSingleRide(RideId);
            ride.RideStatusId = 2;
            await _rideLogic.Save();

            return Redirect("/Driver/Index?UserId=" + UserId);
        } */

        private bool RideExists(int id)
        {
            return _rideLogic.RideExists(id);
        }
    }
}
