using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConestogaCarpool.Models;
using ConestogaCarpool.BusinessLogic;
using Microsoft.AspNetCore.Http;

namespace ConestogaCarpool.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ConestogaCarpoolContext _context;
        private IReviewLogic _reviewLogic;

        public ReviewController(ConestogaCarpoolContext context, IReviewLogic reviewLogic)
        {
            _context = context;
            _reviewLogic = reviewLogic;
        }

        // GET: Review
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

        // GET: Review/PassengerIndex
        public async Task<IActionResult> PassengerIndex(int? PassengerId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.PassengerId = HttpContext.Session.GetInt32("PassengerId");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            TempData["Message"] = null;

            if (PassengerId == null)
            {
                PassengerId = HttpContext.Session.GetInt32("UserId");
            }

            List<Review> passengerReviews = await _reviewLogic.GetPassengerReviews(PassengerId);

            if (passengerReviews.Count == 0)
            {
                TempData["Message"] = "You have not created any reviews";
            }

            return View(passengerReviews);
        }

        // GET: Review/DriverIndex
        public async Task<IActionResult> DriverIndex(int? DriverId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            TempData["Message"] = null;

            List<Review> driverReviews = await _reviewLogic.GetDriverReviews(DriverId);

            if (driverReviews.Count < 1)
            {
                TempData["Message"] = "You have no reviews at this time";
            }
            return View(driverReviews);
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(int? ReviewId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Username = HttpContext.Session.GetString("Username");

            Review review = await _reviewLogic.GetSingleReview(ReviewId);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        public IActionResult Create(int DriverId, int PassengerId, int RideId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (ReviewExistsForRide(RideId))
            {
                TempData["Message"] = "You have already reviewed this ride";
                return RedirectToAction("Details", "Ride", new { RideId = RideId });
            }

            TempData["DriverId"] = DriverId;
            TempData["PassengerId"] = PassengerId;
            TempData["RideId"] = RideId;

            return View();
        }


        // POST: Review/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,Rating,Comment,RideId,PassengerId,DriverId")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.RideId = Int32.Parse(TempData["RideId"].ToString());
                review.PassengerId = Int32.Parse(TempData["PassengerId"].ToString());
                review.DriverId = Int32.Parse(TempData["DriverId"].ToString());

                _reviewLogic.CreateReview(review);
                await _reviewLogic.Save();

                return RedirectToAction(nameof(PassengerIndex));
            }

            return View(review);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(int? ReviewId, int DriverId, int PassengerId, int RideId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (ReviewId == null)
            {
                return NotFound();
            }

            Review review = await _reviewLogic.GetSingleReview(ReviewId);

            if (review == null)
            {
                return NotFound();
            }

            TempData["DriverId"] = DriverId;
            TempData["PassengerId"] = PassengerId;
            TempData["RideId"] = RideId;

            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ReviewId, [Bind("ReviewId,Rating,Comment,RideId,PassengerId,DriverId")] Review review)
        {
            if (ReviewId != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    review.RideId = Int32.Parse(TempData["RideId"].ToString());
                    review.PassengerId = Int32.Parse(TempData["PassengerId"].ToString());
                    review.DriverId = Int32.Parse(TempData["DriverId"].ToString());

                    _reviewLogic.UpdateReview(review);
                    await _reviewLogic.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(PassengerIndex));
            }

            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(int? ReviewId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (ReviewId == null)
            {
                return NotFound();
            }

            Review review = await _reviewLogic.GetSingleReview(ReviewId);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ReviewId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            _reviewLogic.DeleteReview(ReviewId);
            await _reviewLogic.Save();

            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _reviewLogic.ReviewExists(id);
        }

        private bool ReviewExistsForRide(int rideId)
        {
            return _reviewLogic.ReviewExists(rideId);
        }
    }
}