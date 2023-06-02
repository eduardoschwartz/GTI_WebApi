using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GTI_WebApi.Controllers {
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller {

        /// <summary>
        /// Página inicial do swagger
        /// </summary>
        public ActionResult Index() {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
