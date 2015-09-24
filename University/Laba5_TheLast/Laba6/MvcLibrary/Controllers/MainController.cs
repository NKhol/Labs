using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassLibrary;

namespace MvcLibrary.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        DB_CyberneticsEntities ctx = new DB_CyberneticsEntities();

        public ActionResult StartPage()
        {
            return View();
        }

    }
}
