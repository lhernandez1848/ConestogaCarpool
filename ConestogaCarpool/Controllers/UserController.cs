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
using System.Net.Mail;
using System.Net;

namespace ConestogaCarpool.Controllers
{
    public class UserController : Controller
    {
        private readonly ConestogaCarpoolContext _context;
        private IUserLogic _userLogic;

        public UserController(ConestogaCarpoolContext context, IUserLogic userLogic)
        {
            _context = context;
            _userLogic = userLogic;
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            TempData.Clear();
            return RedirectToAction("SignIn", "User");
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(string Username, string Password)
        {
            if (!String.IsNullOrWhiteSpace(Username))
            {
                HttpContext.Session.SetString("Username", Username);
            }

            ViewBag.Username = Username;

            var userSignIn = _userLogic.UserLogin(Username, Password);
            var emailVerified = _userLogic.IsEmailVerified(Username, Password);

            if (userSignIn == false)
            {
                TempData["Message"] = "Incorrect Username or Password";
                return View();
            }
            else if (userSignIn == true && emailVerified == false)
            {
                TempData["Message"] = "Unverified Email";
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        public async void SetUserId(String username)
        {
            User user = await _userLogic.FindUserId(username);

            HttpContext.Session.SetInt32("UserId", user.UserId);
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            string Username = HttpContext.Session.GetString("Username");

            var userLoggedIn = await _userLogic.GetSingleUser(Username);

            if (userLoggedIn.Count == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction(nameof(SignIn));
            }

            SetUserId(Username);

            return View(userLoggedIn);
        }

        public IActionResult PassengerOptions(int? UserId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            ViewBag.Username = HttpContext.Session.GetString("Username");

            return View();
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? UserId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            var user = await _userLogic.ShowDetails(UserId);

            if (user == null)
            {
                TempData["Message"] = "You are not logged in";
                return RedirectToAction(nameof(SignIn));
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Password,Username,FirstName,LastName,Email")] User user, string ConfirmPassword)
        {
            if (user.Password != ConfirmPassword)
            {
                TempData["Message"] = "Passwords do not match";
                return View();
            }
            else if (ModelState.IsValid)
            {
                if (_userLogic.NotEmpty(user) == true && user.Password != null)
                {
                    _userLogic.CreateUser(user);
                }

                await _userLogic.Save();

                HttpContext.Session.SetInt32("TempUserId", user.UserId);

                string verEmail = user.Email;

                _userLogic.SendEmail(verEmail);

                return RedirectToAction(nameof(VerifyEmail));
            }
            return View(user);
        }

        // GET: User/EditVerifyEmail/5
        public async Task<IActionResult> VerifyEmail(int? UserId)
        {
            if (HttpContext.Session.GetInt32("TempUserId") != null)
            {
                UserId = HttpContext.Session.GetInt32("TempUserId");
            }
            else
            {
                TempData["Message"] = "Enter Email";

                return View();
            }

            ViewBag.TempUserId = UserId;

            if (UserId == null)
            {
                TempData["Message"] = "You have not created an account";
                return RedirectToAction(nameof(SignIn));
            }

            var user = await _userLogic.GetUser(UserId);

            if (user == null)
            {
                TempData["Message"] = "You are not logged in";
                return RedirectToAction(nameof(SignIn));
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyEmail(string VerifyCode, string UserEmail, int? UserId, [Bind("VerifiedEmail")] User user)
        {
            UserId = HttpContext.Session.GetInt32("TempUserId");

            user = await _userLogic.FindUserById(UserId);

            if (UserId != user.UserId)
            {
                TempData["Message"] = "You have not created an account";
                return RedirectToAction(nameof(SignIn));
            }
            else if (UserId == null)
            {
                user = await _userLogic.FindUserByEmail(UserEmail);
                HttpContext.Session.SetInt32("TempUserId", user.UserId);
                TempData["Message"] = "";
                return View(nameof(VerifyEmail));
            }

            if (_userLogic.VerifyCode(VerifyCode) == true)
            {
                _userLogic.UpdateEmailVerification(user);
                await _userLogic.Save();
                TempData["Message"] = "Email verified";
                return RedirectToAction(nameof(SignIn));
            }
            else
            {
                TempData["Message"] = "Incorrect verification code entered";
                return View();
            }
        }

        public IActionResult VerifyEmailFromSignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyEmailFromSignIn(string UserEmail)
        {
            var user = await _userLogic.FindUserByEmail(UserEmail);

            if (UserEmail != user.Email)
            {
                TempData["Message"] = "You have not created an account";
                return RedirectToAction(nameof(SignIn));
            }
            else if (UserEmail == null)
            {
                TempData["Message"] = "Enter a valid email address";
                return View();
            }

            HttpContext.Session.SetInt32("TempUserId", user.UserId);
            _userLogic.SendEmail(UserEmail);
            return RedirectToAction(nameof(VerifyEmail));
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? UserId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = UserId;

            var user = await _userLogic.GetUser(UserId);

            if (user == null)
            {
                TempData["Message"] = "You are not logged in";
                return RedirectToAction(nameof(SignIn));
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? UserId, string Username, string FirstName, string LastName,
            [Bind("Username,FirstName,LastName")] User user)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            UserId = ViewBag.UserId;

            user = await _userLogic.FindUserById(UserId);

            if (UserId != user.UserId)
            {
                return NotFound();
            }

            if (_userLogic.NotEmpty(user))
            {
                try
                {
                    user.Username = Username;
                    user.FirstName = FirstName;
                    user.LastName = LastName;

                    _userLogic.UpdateUser(user);

                    HttpContext.Session.SetString("Username", Username);
                    await _userLogic.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: User/Edit/5
        // not yet working
        public async Task<IActionResult> EditPassword(int? UserId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = UserId;

            var user = await _userLogic.GetUser(UserId);

            if (user == null)
            {
                TempData["Message"] = "You are not logged in";
                return RedirectToAction(nameof(SignIn));
            }
            return View(user);
        }

        // POST: User/EditPassword/5
        // not yet working
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(int? UserId, [Bind("Password")] User user, string OldPassword, string NewPassword, string ConfirmNewPassword)
        {
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            UserId = ViewBag.UserId;

            user = await _userLogic.FindUserById(UserId);

            if (UserId != user.UserId)
            {
                return NotFound();
            }
            if (_userLogic.OldPasswordIsMatch(UserId, OldPassword) == true)
            {
                if (NewPassword == ConfirmNewPassword)
                {
                    try
                    {
                        user.Password = PasswordHash.HashPassword(NewPassword);
                        _userLogic.UpdateUser(user);
                        await _userLogic.Save();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.UserId))
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
            }

            return View(user);
        }


        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? UserId)
        {
            if (HttpContext.Session.GetInt32("UserId") == null || HttpContext.Session.GetInt32("UserId") == 0)
            {
                TempData["Message"] = "You must be logged in to access that page";
                return RedirectToAction("SignIn", "User");
            }

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            var user = await _userLogic.ShowDetails(UserId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int UserId)
        {
            _userLogic.DeleteUser(UserId);
            await _userLogic.Save();
            return SignOut();
        }

        private bool UserExists(int id)
        {
            return _userLogic.UserExists(id);
        }
    }
}
