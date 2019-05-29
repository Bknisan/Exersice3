using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace Exersice3.Models
{
    public class localClient
    {
        public bool canRead = true;
        private bool connected = false;
        public event PropertyChangedEventHandler propChanged;
        public double lon { get; set; }
        public double lat { get; set; }

        public localClient()
        {
           
        }
        // request function.
        public void Request(string ip, int port)
        {
            // open stream socket with tcp protocol on the same computer.
            Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // connect to local host port number 5400
            while (!connected)
            {
                try
                {
                    connected = true;
                    mySocket.Connect(new IPEndPoint((IPAddress.Parse(ip)), port));
                }
                catch(Exception)
                {
                    connected = false;
                }
            }
            while (canRead)
            {
                // send get request specifeid by the value path.
                mySocket.Send(System.Text.Encoding.ASCII.GetBytes("get /position/longitude-deg\r\n"));
                byte[] messege = new byte[512];
                // translate byte array to string
                string longi = System.Text.Encoding.ASCII.GetString(messege, 0, mySocket.Receive(messege));
                // clean recieved string.
                lon = Double.Parse(((Regex.Match(longi, @"'(.*?[^\\])'")).Value).Trim('\''));
                mySocket.Send(System.Text.Encoding.ASCII.GetBytes("get /position/latitude-deg\r\n"));
                Array.Clear(messege, 0, messege.Length);
                string lati = System.Text.Encoding.ASCII.GetString(messege, 0, mySocket.Receive(messege));
                lat = Double.Parse(((Regex.Match(lati, @"'(.*?[^\\])'")).Value).Trim('\''));
                propChanged?.Invoke(this, new PropertyChangedEventArgs(lon + "," + lat));
                // sleep for 250 miliseconds.
                Thread.Sleep(250);
            }
        }
    }
}