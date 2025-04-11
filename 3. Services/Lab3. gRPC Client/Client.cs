using Grpc.Net.Client;
using Lab3._gRPC_Client;

namespace Lab3._gRPC_Client
{
    internal class Client
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7250");

            var client = new Greeter.GreeterClient(channel);

            Console.Write("Введите имя: ");
            var name = Console.ReadLine();

            var reply = await client.SayHelloAsync(new HelloRequest { Name = name });

            Console.WriteLine($"Ответ от сервера: {reply.Message}");
            Console.ReadKey();
        }
    }
}
