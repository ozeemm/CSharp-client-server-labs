using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Lab4._SocketClient
{
    internal class Program
    {
        private const string ServerIP = "127.0.0.1";
        private const int Port = 11000;

        static async Task Main(string[] args)
        {
            Console.WriteLine("0 - ручное общение. 1 - автоматические запросы. ");
            Console.Write("Ввод: "); 
            var choice = Console.ReadLine();

            if (choice == "0")
                await HandSpeaking();
            else if (choice == "1")
                await AutoSpeaking();
        }

        static async Task HandSpeaking()
        {
            while (true)
            {
                Console.Write("Ввод: ");
                var input = Console.ReadLine();
                if (input == "")
                    break;

                await SendRequestAsync(input);
            }
        }

        static async Task AutoSpeaking(int clientsCount = 20)
        {
            Console.WriteLine($"Запуск {clientsCount} клиентов");
            var tasks = new Task[clientsCount];
            for(int i = 0; i < clientsCount; i++)
            {
                tasks[i] = Task.Run(() => SendRequestAsync($"Сообщение №{i + 1}"));

            }

            await Task.WhenAll(tasks);
            Console.WriteLine("Done!");
        }

        static async Task SendRequestAsync(string content)
        {
            using(var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                await clientSocket.ConnectAsync(new IPEndPoint(IPAddress.Parse(ServerIP), Port));
                
                byte[] requestData = Encoding.UTF8.GetBytes(content);
                await clientSocket.SendAsync(new ArraySegment<byte>(requestData), SocketFlags.None);
                Console.WriteLine("Отправлено");

                var buffer = new byte[1024];
                int received = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                string response = Encoding.UTF8.GetString(buffer, 0, received);
                Console.WriteLine($"Получен ответ:\n{response}");
            }
        }
    }
}
