using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Discipline.Web.Models.ViewModels;
using Discipline.Domain.Entities;
using Discipline.Domain.Abstract;
using Discipline.Domain.Concrete;
using System.Data.Entity.Migrations;
using System.Collections.Generic;

namespace Discipline.Web.Controllers {
    [Authorize]
    public class AccountController : Controller {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController() { }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) {
            UserManager = userManager;
            SignInManager = signInManager;

            // Check if there is an admin account, if not, initialize one with the default password
            if (!userManager.Users.Any(u => u.UserName == "admin@discipline.com")) {
                var admin = new ApplicationUser { UserName = "admin@discipline.com" };
                userManager.Create(admin, "Password@123");
                userManager.AddToRole(admin.Id, "Admin");
            }
        }

        public ApplicationSignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff() {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            // Will only work the first time the server is started and an admin account hasn't been created
            if (model.Email == "admin@discipline.com" && !UserManager.Users.Any(u => u.UserName == "admin@discipline.com")) {
                return View("InitialAdmin");
            }

            if (model.Email == "test@discipline.com") {
                // Ugly hack
                SeedTestUserData(new ApplicationDbContext());
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result) {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAdmin(RegisterViewModel model) {
            if (ModelState.IsValid) {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    result = await UserManager.AddToRoleAsync(user.Id, "Admin");
                    if (result.Succeeded) {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View("InitialAdmin", model);
        }

        //
        // GET: /Account/Register
        [Authorize(Roles = "Admin")]
        public ActionResult Register() {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    result = await UserManager.AddToRoleAsync(user.Id, "User");
                    if (result.Succeeded) {
                        //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Home");
                    }
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (_userManager != null) {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null) {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null) {
            }

            public ChallengeResult(string provider, string redirectUri, string userId) {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context) {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null) {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        private Random random = new Random();
        private int minuteRandomizer(out int lastMinute) => lastMinute = this.random.Next(0, 59);

        private void SeedTestUserData(ApplicationDbContext context) {
            // Purge
            var activities = context.Activities.Where(a => a.UserName == "test@discipline.com").ToList();
            var durationsToRemove = new List<Duration>();
            foreach (var activity in activities)
                durationsToRemove.AddRange(context.Durations.Where(d => d.ActivityId == activity.Id).ToList());

            context.Durations.RemoveRange(durationsToRemove);
            context.Activities.RemoveRange(activities);

            // Populate
            context.Activities.AddRange(new List<Activity> {
                new Activity { UserName = "test@discipline.com", Name = "Sleeping", Description = "I love sleeping!", Color = "#0099ff" },
                new Activity { UserName = "test@discipline.com", Name = "Eating", Description = "*chomp chomp mnggff!*", Color = "#009966" },
                new Activity { UserName = "test@discipline.com", Name = "Coding", Description = "*klik klak klik klik*", Color = "#3300cc" },
                new Activity { UserName = "test@discipline.com", Name = "Reading", Description = "*...page flip*", Color = "#333300" },
                new Activity { UserName = "test@discipline.com", Name = "Gaming", Description = "*pew pew pew!*", Color = "#6600cc", BudgetHours = 3 }
            });

            context.SaveChanges();

            var dictOfAllTestUserActivities = new Dictionary<string, int> {
                { "Sleeping", context.Activities.Where(a => a.Name == "Sleeping" && a.UserName == "test@discipline.com").First().Id },
                { "Coding", context.Activities.Where(a => a.Name == "Coding" && a.UserName == "test@discipline.com").First().Id },
                { "Reading", context.Activities.Where(a => a.Name == "Reading" && a.UserName == "test@discipline.com").First().Id },
                { "Eating", context.Activities.Where(a => a.Name == "Eating" && a.UserName == "test@discipline.com").First().Id },
                { "Gaming", context.Activities.Where(a => a.Name == "Gaming" && a.UserName == "test@discipline.com").First().Id }
            };

            int lastMinute;
            var result = new List<Duration>() { };
            for (DateTime d = (DateTime.Today).AddDays(-30); d < DateTime.Today; d = d.AddDays(1)) {
                result.Add(new Duration { ActivityId = dictOfAllTestUserActivities["Sleeping"], From = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0), To = new DateTime(d.Year, d.Month, d.Day, 8, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = dictOfAllTestUserActivities["Coding"], From = new DateTime(d.Year, d.Month, d.Day, 8, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 10, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = dictOfAllTestUserActivities["Eating"], From = new DateTime(d.Year, d.Month, d.Day, 10, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 11, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = dictOfAllTestUserActivities["Coding"], From = new DateTime(d.Year, d.Month, d.Day, 11, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 15, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = dictOfAllTestUserActivities["Gaming"], From = new DateTime(d.Year, d.Month, d.Day, 15, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 17, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = dictOfAllTestUserActivities["Coding"], From = new DateTime(d.Year, d.Month, d.Day, 17, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 19, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = dictOfAllTestUserActivities["Eating"], From = new DateTime(d.Year, d.Month, d.Day, 19, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 20, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = dictOfAllTestUserActivities["Gaming"], From = new DateTime(d.Year, d.Month, d.Day, 20, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 22, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = dictOfAllTestUserActivities["Reading"], From = new DateTime(d.Year, d.Month, d.Day, 22, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 23, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = dictOfAllTestUserActivities["Sleeping"], From = new DateTime(d.Year, d.Month, d.Day, 23, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 23, 59, 59) });
            }

            context.Durations.AddRange(result);
            context.Activities.Find(dictOfAllTestUserActivities["Coding"]).Start = DateTime.Now.AddHours(-1);

            context.SaveChanges();
            context.Dispose();
        }
        #endregion
    }
}