using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Lab4._SocketServer
{
    class SocketServerAsync
    {
        private const int port = 11000;
        private int requestCount = 0;

        public async Task Start()
        {
            Console.WriteLine("Многопоточный сервер запущен");
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Any, port));
            listener.Listen(100); // Максимальная очередь подключений

            using (listener)
            {
                while (true)
                {
                    var clientSocket = await listener.AcceptAsync();
                    await HandleClientAsync(clientSocket);
                }
            }
        }

        public async Task HandleClientAsync(Socket clientSocket)
        {
            requestCount++;
            Console.WriteLine($"Обработка запроса #{requestCount} в потоке {Environment.CurrentManagedThreadId}");

            // Получение данных
            var buffer = new byte[1024];
            int received = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
            string request = Encoding.UTF8.GetString(buffer, 0, received);
            Console.WriteLine($"Получено: {request}");

            // Отправка ответа
            string response = $"[Error]: Неизвестная команда: '{request}'. А в прочем, я совсем не знаю никаких команд.";
            byte[] responseData = Encoding.UTF8.GetBytes(response);
            await clientSocket.SendAsync(new ArraySegment<byte>(responseData), SocketFlags.None);

            clientSocket.Shutdown(SocketShutdown.Both);
        }
    }
}
