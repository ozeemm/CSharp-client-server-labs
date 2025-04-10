using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab4._SocketServer
{
    class SocketServer
    {
        private const int port = 11000;
        private int requestCount = 0;

        public void Start()
        {
            Console.WriteLine("Однопоточный сервер запущен");

            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Any, port));
            listener.Listen(10); // Максимальная очередь подключений

            using (listener)
            {
                while (true)
                {
                    Console.WriteLine("Ожидание подключения");
                    using (var clientSocket = listener.Accept())
                    {
                        requestCount++;
                        Console.WriteLine($"Обработка запроса №{requestCount}");

                        // Получение данных
                        var buffer = new byte[1024];
                        int received = clientSocket.Receive(buffer);
                        string request = Encoding.UTF8.GetString(buffer, 0, received);
                        Console.WriteLine($"Получено: {request}");

                        // Отправка ответа
                        string response = $"[Error]: Неизвестная команда: '{request}'. А в прочем, я совсем не знаю никаких команд.";
                        byte[] responseData = Encoding.UTF8.GetBytes(response);
                        clientSocket.Send(responseData);

                        clientSocket.Shutdown(SocketShutdown.Both);
                    }
                }
            }
        }
    }
}
