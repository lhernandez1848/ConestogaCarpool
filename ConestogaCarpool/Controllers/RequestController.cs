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
using ConestogaCarpool.Repositories;

namespace ConestogaCarpool.Controllers
{
    public class RequestController : Controller
    {
        private readonly ConestogaCarpoolContext _context;
        private IRequestLogic _requestLogic;

        public RequestController(ConestogaCarpoolContext context, IRequestLogic requestLogic)
        {
            _context = context;
            _requestLogic = requestLogic;
        }

        // GET: Requests
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

        // GET: Request
        public async Task<IActionResult> PassengerIndex(int? PassengerId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            TempData["Message"] = null;

            List<Request> passengerRequests = await _requestLogic.GetPassengerRequests(PassengerId);

            if (passengerRequests.Count < 1)
            {
                TempData["Message"] = "You do not have any ride requests";
            }

            return View(passengerRequests);
        }

        // GET: Request
        public async Task<IActionResult> DriverIndex(int? DriverId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            TempData["Message"] = null;

            List<Request> driverRequests = await _requestLogic.GetDriverRequests(DriverId);

            if (driverRequests.Count < 1)
            {
                TempData["Message"] = "You do not have any pending ride requests";
            }

            return View(driverRequests);
        }

        // GET: Request/Details/5
        public async Task<IActionResult> Details(int? RequestId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (RequestId == null)
            {
                return NotFound();
            }

            Request request = await _requestLogic.GetSingleRequest(RequestId);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Request/Create
        public async Task<IActionResult> Create(int? PostId, string DriverEmail)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            var request = await _requestLogic.GetPost(PostId);

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.DriverEmail = DriverEmail;
            ViewBag.PostId = PostId;

            return View(request);
        }

        // POST: Request/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,RequestStatusId,PassengerId,PostId")] Request request, string DriverEmail)
        {
            string Username = HttpContext.Session.GetString("Username");

            int? PassengerId = HttpContext.Session.GetInt32("UserId");

            if (ModelState.IsValid)
            {
                request.PassengerId = Convert.ToInt32(PassengerId);
                _requestLogic.CreateRequest(request);
                await _requestLogic.Save();

                string body = @"<h2>You have a ride request by " + Username + " </h2><br />" +
                "<p>Please log in to view this request </p>";
                string subject = "Conestoga Carpool -- Ride Requested";

                _requestLogic.SendEmail(subject, body, DriverEmail);

                return RedirectToAction(nameof(PassengerIndex), new { PassengerId = request.PassengerId });
            }

            return View(request);
        }

        // GET: Request/Edit/5
        public async Task<IActionResult> Edit(int? RequestId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (RequestId == null)
            {
                return NotFound();
            }

            Request request = await _requestLogic.GetSingleRequest(RequestId);

            if (request == null)
            {
                return NotFound();
            }
            ViewData["PassengerId"] = new SelectList(_context.User, "UserId", "Email", request.PassengerId);
            ViewData["PostId"] = new SelectList(_context.Post, "PostId", "Destination", request.PostId);
            ViewData["RequestStatusId"] = new SelectList(_context.RequestStatus, "RequestStatusId",
                "RequestStatusDescription", request.RequestStatusId);
            return View(request);
        }

        // POST: Request/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int RequestId, [Bind("RequestId,RequestStatusId,PassengerId,PostId")] Request request)
        {
            if (RequestId != request.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _requestLogic.UpdateRequest(request);
                    await _requestLogic.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.RequestId))
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
            ViewData["PassengerId"] = new SelectList(_context.User, "UserId", "Email", request.PassengerId);
            ViewData["PostId"] = new SelectList(_context.Post, "PostId", "Destination", request.PostId);
            ViewData["RequestStatusId"] = new SelectList(_context.RequestStatus, "RequestStatusId",
                "RequestStatusDescription", request.RequestStatusId);
            return View(request);
        }

        // GET: Request/Delete/5
        public async Task<IActionResult> Delete(int? RequestId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (RequestId == null)
            {
                return NotFound();
            }

            Request request = await _requestLogic.GetSingleRequest(RequestId);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int RequestId)
        {
            _requestLogic.DeleteRequest(RequestId);
            await _requestLogic.Save();
            return RedirectToAction("Index", "User");

            // NOTE:
            // If cancelRequest, change return RedirectToAction(nameof(Index)); to PassengerIndex

        }


        // GET: Request/Accept/5
        public async Task<IActionResult> Accept(int? RequestId, string PassengerEmail)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.PassengerEmail = PassengerEmail;

            if (RequestId == null)
            {
                return NotFound();
            }

            Request request = await _requestLogic.GetSingleRequest(RequestId);

            if (request == null)
            {
                return NotFound();
            }
            ViewData["PassengerId"] = new SelectList(_context.User, "UserId", "Email", request.PassengerId);
            ViewData["PostStatusId"] = new SelectList(_context.PostStatus, "PostStatusId", "PostStatusDescription", request.Post.PostStatusId);
            ViewData["RequestStatusId"] = new SelectList(_context.RequestStatus, "RequestStatusId", "RequestStatusDescription", request.RequestStatusId);
            return View(request);
        }

        // POST: Request/Accept/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int RequestId, [Bind("RequestId,RequestStatusId,PassengerId,PostId")] Request request,
            string PassengerEmail)
        {
            string Username = HttpContext.Session.GetString("Username");

            if (RequestId != request.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    /*request = await _context.Request
                        .Include(r => r.Passenger)
                        .Include(r => r.Post)
                        .Include(r => r.RequestStatus)
                        .FirstOrDefaultAsync(m => m.RequestId == RequestId);*/

                    request = await _requestLogic.GetSingleRequest(RequestId);

                    _requestLogic.AcceptRequest(request);
                    await _requestLogic.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.RequestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                string body = @"<h2>Your ride request to " + Username + " was accepted</h2><br />" +
                "<p>Please log in to view this request.</p><p>Your driver will be waiting outside your campus " +
                "location by the specified time</p>";
                string subject = "Conestoga Carpool -- Request Accepted";

                // Send confirmation email to passenger
                _requestLogic.SendEmail(subject, body, PassengerEmail);

                // Redirect to Create Ride
                return RedirectToAction(nameof(Create), "Ride", new { postId = request.PostId, requestId = request.RequestId });

                // Redirect to driverIndex
                //return RedirectToAction(nameof(DriverIndex), new { DriverId = request.Post.DriverId });
            }
            ViewData["PassengerId"] = new SelectList(_context.User, "UserId", "Email", request.PassengerId);
            ViewData["PostStatusId"] = new SelectList(_context.PostStatus, "PostStatusId", "PostStatusDescription",
                request.Post.PostStatusId);
            ViewData["RequestStatusId"] = new SelectList(_context.RequestStatus, "RequestStatusId", "RequestStatusDescription",
                request.RequestStatusId);
            return View(request);
        }

        // GET: Request/Decline/5
        public async Task<IActionResult> Decline(int? RequestId, string PassengerEmail)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.PassengerEmail = PassengerEmail;

            if (RequestId == null)
            {
                return NotFound();
            }

            Request request = await _requestLogic.GetSingleRequest(RequestId);

            if (request == null)
            {
                return NotFound();
            }
            ViewData["PassengerId"] = new SelectList(_context.User, "UserId", "Email", request.PassengerId);
            ViewData["PostStatusId"] = new SelectList(_context.PostStatus, "PostStatusId", "PostStatusDescription", request.Post.PostStatusId);
            ViewData["RequestStatusId"] = new SelectList(_context.RequestStatus, "RequestStatusId", "RequestStatusDescription", request.RequestStatusId);
            return View(request);
        }

        // POST: Request/Decline/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Decline(int RequestId, [Bind("RequestId,RequestStatusId,PassengerId,PostId")] Request request,
            string PassengerEmail)
        {
            if (RequestId != request.RequestId)
            {
                return NotFound();
            }

            string Username = HttpContext.Session.GetString("Username");

            if (ModelState.IsValid)
            {
                try
                {
                    /*request = await _context.Request
                        .Include(r => r.Passenger)
                        .Include(r => r.Post)
                        .Include(r => r.RequestStatus)
                        .FirstOrDefaultAsync(m => m.RequestId == RequestId);*/

                    request = await _requestLogic.GetSingleRequest(RequestId);

                    _requestLogic.DeclineRequest(request);
                    await _requestLogic.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.RequestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                string body = @"<h2>Your ride request to " + Username + " was declined</h2><br />" +
                "<p>You may log in to try looking for another driver</p>";
                string subject = "Conestoga Carpool -- Request Declined";

                // Send email to passenger
                _requestLogic.SendEmail(subject, body, PassengerEmail);

                // Redirect to driverIndex
                return RedirectToAction(nameof(DriverIndex), new { DriverId = request.Post.DriverId });
            }

            ViewData["PassengerId"] = new SelectList(_context.User, "UserId", "Email", request.PassengerId);
            ViewData["PostStatusId"] = new SelectList(_context.PostStatus, "PostStatusId", "PostStatusDescription", request.Post.PostStatusId);
            ViewData["RequestStatusId"] = new SelectList(_context.RequestStatus, "RequestStatusId", "RequestStatusDescription", request.RequestStatusId);

            return View(request);
        }

        private bool RequestExists(int id)
        {
            return _requestLogic.RequestExists(id);
        }
    }
}