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
        private bool readerAlreadyWorking = false;
        private bool firstRead = false;
        localClient application;
        private System.Timers.Timer interval;
        [HttpGet]
        public ActionResult display(string ip, int port)
        {
            if (!readerAlreadyWorking)
            {
                application = new localClient();
                // run this function when parameters updated.
                application.propChanged += Handler;
                Task clientReader = new Task(() =>
                {
                    application.Request(ip, port);
                });
                // start reading.
                clientReader.Start();
                readerAlreadyWorking = true;
            }
            // wait until first update.(just for the first time.)
            while (!firstRead) ;
            return View();
        }

        public ActionResult Refresh(string ip, int port, int timeSlice)
        {
            if (!readerAlreadyWorking)
            {
                application = new localClient();
                // run this function when parameters updated.
                application.propChanged += Handler;
                Task clientReader = new Task(() =>
                {
                    application.Request(ip, port);
                });
                // start reading.
                clientReader.Start();
                readerAlreadyWorking = true;
            }
            while (!firstRead);
            return View();
        }

        [HttpGet]
        public ActionResult save(string ip, int port,int timeSlice,int timePeriod, string file)
        {
            // new instance of display action result.
            ActionResult newView = display(ip, port);
            interval = new Timer();
            // set time interval as 4 times a second.
            interval.Interval = (1000 / timeSlice);
            interval.Enabled = true;
            // call simple display function.
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
            ViewBag.longitude = application.lon;
            ViewBag.latitude = application.lat;
            firstRead = true;
        }

    }
}