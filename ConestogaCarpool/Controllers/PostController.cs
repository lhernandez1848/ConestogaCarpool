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
    public class PostController : Controller
    {
        private readonly ConestogaCarpoolContext _context;
        private IPostLogic _postLogic;

        public PostController(ConestogaCarpoolContext context, IPostLogic postLogic)
        {
            _context = context;
            _postLogic = postLogic;
        }

        // GET: Post
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

        public async Task<IActionResult> PassengerIndex(string Location, string Destination)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            TempData["Message"] = null;

            List<Post> availableDrivers = await _postLogic.GetAvailableDrivers(Location, Destination);

            HttpContext.Session.SetString("Location", Location);
            HttpContext.Session.SetString("Destination", Destination);

            if (availableDrivers.Count < 1)
            {
                TempData["Message"] = "There are no available drivers at this time";
            }

            return View(availableDrivers);
        }

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

            List<Post> driverPosts = await _postLogic.GetDriverPosts(DriverId);

            if (driverPosts.Count < 1)
            {
                TempData["Message"] = "There are no available drivers at this time";
            }
            return View(driverPosts);
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? PostId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.DriverId = HttpContext.Session.GetInt32("DriverId");

            if (PostId == null)
            {
                return NotFound();
            }

            Post post = await _postLogic.GetSinglePost(PostId);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Post/Create
        public IActionResult Create(int? VehicleId, int? DriverId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            // Get userId from session
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            HttpContext.Session.SetInt32("VehicleId", Convert.ToInt32(VehicleId));
            HttpContext.Session.SetInt32("DriverId", Convert.ToInt32(DriverId));

            // Save vehicleId to viewbag
            ViewBag.VehicleId = VehicleId;

            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,PostStatusId,DriverId,VehicleId,Destination,Location,Date,Time")] Post post)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.VehicleId = HttpContext.Session.GetInt32("VehicleId");
            ViewBag.DriverId = HttpContext.Session.GetInt32("DriverId");

            post.DriverId = ViewBag.DriverId;
            post.VehicleId = ViewBag.VehicleId;

            if (ModelState.IsValid)
            {
                _postLogic.CreatePost(post);
                await _postLogic.Save();

                return RedirectToAction(nameof(DriverIndex), new { driverId = post.DriverId });
            }
            //ViewData["DriverId"] = new SelectList(_context.User, "UserId", "Email", post.DriverId);
            //ViewData["PostStatusId"] = new SelectList(_context.PostStatus, "PostStatusId", "PostStatusDescription", post.PostStatusId);
            //ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Make", post.VehicleId);
            return View(post);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? PostId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (PostId == null)
            {
                return NotFound();
            }

            Post post = await _postLogic.GetSinglePost(PostId);

            if (post == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.User, "UserId", "Email", post.DriverId);
            ViewData["PostStatusId"] = new SelectList(_context.PostStatus, "PostStatusId", "PostStatusDescription", post.PostStatusId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Make", post.VehicleId);
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int PostId, [Bind("PostId,PostStatusId,DriverId,VehicleId,Destination,Location,Date,Time")] Post post)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            ViewBag.DriverId = HttpContext.Session.GetInt32("DriverId");

            post.DriverId = ViewBag.DriverId;

            if (PostId != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _postLogic.UpdatePost(post);
                    await _postLogic.Save();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Get userId from session variable
                //var userId = HttpContext.Session.GetInt32("userId");

                // redirect to DriverIndex
                // return RedirectToAction(nameof(DriverIndex), new { driverId = userId });

                return RedirectToAction(nameof(Index));
            }

            ViewData["DriverId"] = new SelectList(_context.User, "UserId", "Email", post.DriverId);
            ViewData["PostStatusId"] = new SelectList(_context.PostStatus, "PostStatusId", "PostStatusDescription", post.PostStatusId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Make", post.VehicleId);
            return View(post);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? PostId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (PostId == null)
            {
                return NotFound();
            }

            Post post = await _postLogic.GetSinglePost(PostId);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int PostId)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            _postLogic.DeletePost(PostId);
            await _postLogic.Save();

            // Get userId from session variable
            //var userId = HttpContext.Session.GetInt32("userId");

            // redirect to Index
            // return RedirectToAction(nameof(DriverIndex), new { driverId = userId });
            return RedirectToAction(nameof(Index), "User");
        }

        public IActionResult FindDriver()
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            return View();
        }

        // GET: Post
        public async Task<IActionResult> SendRequest(int? PostId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            if (PostId == null)
            {
                return NotFound();
            }

            Post post = await _postLogic.GetSinglePost(PostId);

            if (post == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("PostId", PostId.ToString());

            return View(post);
        }

        private bool PostExists(int id)
        {
            return _postLogic.PostExists(id);
        }
    }
}
