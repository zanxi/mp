using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Xml;

namespace helper
{
    public class Dumper
    {
        
        // адрес и порт сервера, к которому будем подключаться
        int port = 1080; // порт сервера
        string address = "192.168.1.188"; // адрес сервера
        int max_id;
        bool connect=false;
        Thread getPhoto;
        Socket socket;
        String[] values;
        int step_id = 1200;
        public Dumper(string address, int port, int max_id)
        {
            this.address = address;
            this.port = port;
            this.max_id = max_id;
            values = new string[max_id+1];
        }
        public string getValue (int id)
        {
            return values[id];
        }
        public bool isConnected()
        {
            return connect;
        }
        public bool Connect()
        {
            connect = false;
            try
            {
//                socket.ReceiveTimeout = 1;
                connect = true;
                getPhoto = new Thread(Run);
                getPhoto.Start();
            }
            catch (Exception ex)
            {
                Util.errorMessage("Dump",ex.Message);
            }
            return connect;

        }
        public void Close()
        {
            connect = false;
            getPhoto.Abort();
        }
        void Run()
        {
            byte[] data = new byte[128000]; // буфер для ответа
            int bytes = 0; // количество полученных байт
            try
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    int id = 1;
                    while (id <= max_id)
                    {
                        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        // подключаемся к удаленному хосту
                        socket.Connect(ipPoint);
                        StringBuilder builder = new StringBuilder();
                        byte[] breq= Encoding.ASCII.GetBytes(id + " " + (id + step_id));
                        socket.Send(breq);
                        do
                        {
                            bytes = socket.Receive(data, data.Length, 0);
                            builder.Append(Encoding.ASCII.GetString(data, 0, bytes));
                        }
                        while (!builder.ToString().Contains("</vals>"));
                        XmlDocument xmls = new XmlDocument();
                        xmls.LoadXml(builder.ToString());
                        foreach (XmlNode v in xmls.SelectNodes("vals/val"))
                        {
                            values[int.Parse(v.Attributes["id"].Value)]= v.Attributes["value"].Value;
                        }
                        id += step_id;
                        socket.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                if (!connect) return;
                Util.message(ex.Message);
                socket.Close();
                connect = false;
            }
        }
    }

}

