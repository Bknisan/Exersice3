using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using Exersice3.Models;

namespace Exersice3.Controllers
{
    public class HomeController : Controller
    {
       [HttpGet]
        public ActionResult display(string ip, int port)
        {
            return View();
        }
        [HttpGet]
        public ActionResult save(string ip, int port, string fileName)
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}