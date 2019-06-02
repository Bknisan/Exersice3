using System;
using System.IO;
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
        private double lon;
        private double lat;
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
            readerAlreadyWorking = true;
            // wait until first update.(just for the first time.)
            while (!firstRead) ;
            return View();
        }

        [HttpGet]
        public ActionResult save(string ip, int port,int timeSlice,int timePeriod, string file)
        {
            ViewBag.period = timePeriod * 1000;
            ViewBag.interval = (1000 / timeSlice);
            ViewBag.fileName = file;
            // create empty text file with specified name and closing it.
            System.IO.File.Create(file).Close();
            if (!readerAlreadyWorking)
            {
                // run this function when parameters updated.
                localClient.Instance.propChanged += Handler;
            }
            localClient.Instance.Request(ip, port);
            readerAlreadyWorking = true;
            // wait until first update.(just for the first time.)
            while (!firstRead) ;
            return View();
        }


        // run this function when property changed.
        public void Handler(object sender, PropertyChangedEventArgs args)
        {
            string[] vals = (args.PropertyName).Split(',');
            // update view bag.
            ViewBag.longitude = Double.Parse(vals[0]);
            ViewBag.latitude = Double.Parse(vals[1]);
            firstRead = true;
        }

        [HttpPost]
        public string Position()
        {
            double[] vals = localClient.Instance.Request("127.0.0.1", 5400);
            CalculatePos position = new CalculatePos(vals[0],vals[1]);
            var json = new JavaScriptSerializer().Serialize(position);
            return json;

        }

        [HttpPost]
        public void WriteData(string fileName)
        {
           // get data.
           string data =  localClient.Instance.RequestAdditional();
           data += "\r\n";
           byte[] info = System.Text.Encoding.ASCII.GetBytes(data);
           FileStream writer = new FileStream(fileName, FileMode.Append);
           writer.Write(info,0,data.Length);
        }

    }
}