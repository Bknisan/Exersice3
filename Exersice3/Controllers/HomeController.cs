using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Exersice3.Models;

namespace Exersice3.Controllers
{
    public class HomeController : Controller
    {
        private bool readerAlreadyWorking = false;
        private bool firstRead = false;
        private System.Timers.Timer interval;



        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult display(string ip, int port)
        {
            if (!readerAlreadyWorking)
            {
                // run this function when parameters updated.
                localClient.Instance.propChanged += Handler;
            }
                localClient.Instance.Request(ip, port);              
                // start reading.
                readerAlreadyWorking = true;
            // wait until first update.(just for the first time.)
            while (!firstRead) ;
            return View();
        }
        [HttpGet]
        public ActionResult Refresh(string ip, int port, int timeSlice)
        {
            ViewBag.interval = (1000 / timeSlice);
            if (!readerAlreadyWorking)
            {
                // run this function when parameters updated.
                localClient.Instance.propChanged += Handler;
            }
            localClient.Instance.Request(ip, port);
            // start reading.
            readerAlreadyWorking = true;
            // wait until first update.(just for the first time.)
            while (!firstRead) ;
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


        // run this function when property changed.
        public void Handler(object sender, PropertyChangedEventArgs args)
        {
            // update view bag.
            ViewBag.longitude = localClient.Instance.lon;
            ViewBag.latitude = localClient.Instance.lat;
            firstRead = true;
        }


        [HttpPost]
        public string Position()
        {
            localClient.Instance.Request("127.0.0.1", 5400);
            CalculatePos position = new CalculatePos(localClient.Instance.lon, localClient.Instance.lat);
            var json = new JavaScriptSerializer().Serialize(position);
            return json;

        }

    }
}