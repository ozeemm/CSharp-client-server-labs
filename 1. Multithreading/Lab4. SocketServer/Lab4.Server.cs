namespace Lab4._SocketServer
{
    internal class Lab4
    {
        static async Task Main(string[] args)
        {
            var server = new SocketServerAsync();
            await server.Start();
        }
    }
}
