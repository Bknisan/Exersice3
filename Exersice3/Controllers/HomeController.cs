using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using Exersice3.Models;

namespace Exersice3.Controllers
{
    public class HomeController : Controller
    {
        private bool firstRead = false;
        localClient application;
       [HttpGet]
        public ActionResult display(string ip, int port)
        {
            application = new localClient();
            application.propChanged += Handler;
            Task startReading = new Task(() =>
            {
                application.Request();
            });
            startReading.Start();
            // wait until first update.
            while (!firstRead) ;
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

        // run this function when property changed.
        public void Handler(object sender, PropertyChangedEventArgs args)
        {
            // update view bag.
            ViewBag.longitude = CalculatePos.normLongi(application.lon);
            ViewBag.latitude = CalculatePos.normLati(application.lat);
            firstRead = true;
        }

    }
}