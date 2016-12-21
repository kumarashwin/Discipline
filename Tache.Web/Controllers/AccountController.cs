using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tache.Controllers {

    [AllowAnonymous]
    public class AccountController : Controller {

        public ActionResult Login() {
            return View();
        }
    }
}