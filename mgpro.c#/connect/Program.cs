using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Xml;


namespace connect
{
    class Program
    {
        // адрес и порт сервера, к которому будем подключаться
        static int port = 1080; // порт сервера
        static string address = "192.168.1.188"; // адрес сервера
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                while (true)
                {
                    Console.Write("Введите сообщение:");
                    string message = Console.ReadLine();
                    if (message.Length == 0) break;
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    socket.Send(data);

                    // получаем ответ
                    data = new byte[30000]; // буфер для ответа
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байт

                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.ASCII.GetString(data, 0, bytes));
                    }
                    while (socket.Available > 0);
                    Console.WriteLine("ответ сервера: " + builder.ToString());
                    XmlDocument xmls = new XmlDocument();
                    xmls.LoadXml(builder.ToString());
                    foreach(XmlNode v in xmls.SelectNodes("vals/val"))
                    {

                        
                    }

                }

                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
