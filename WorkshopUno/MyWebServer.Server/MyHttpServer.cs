using System;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using MyWebServer.Server.Http;
using MyWebServer.Server.Routing;

namespace MyWebServer.Server
{
    public class MyHttpServer
    {
        private readonly TcpListener listener;
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly RoutingTable routingTable;

        public MyHttpServer(string ipAddress, int port, Action<IRoutingTable> routingTableConfiguration)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            this.listener = new TcpListener(this.ipAddress, this.port);

            this.routingTable = new RoutingTable();
            routingTableConfiguration(this.routingTable);
        }

        public MyHttpServer(int port, Action<IRoutingTable> routingTable)
            : this("127.0.0.1", port, routingTable)
        {
        }

        public MyHttpServer(Action<IRoutingTable> routingTable)
            : this(8888, routingTable)
        {
        }

        public async Task Start()
        {
            this.listener.Start();

            Console.WriteLine($"Server is listening...");
            
            while (true)
            {
                _ = Task.Run(async () =>
                {
                    var connection = await this.listener.AcceptTcpClientAsync();

                    var networkStream = connection.GetStream();

                    var requestText = await this.ReadRequest(networkStream);

                    try
                    {
                        var request = HttpRequest.Parse(requestText);

                        var response = this.routingTable.ExecuteRequest(request);

                        this.PrepareSession(request, response);

                        this.LogPipeline(request, response);

                        await this.WriteResponse(networkStream, response);
                    }
                    catch (Exception exception)
                    {
                        await this.HandleError(networkStream, exception);
                    }

                    connection.Close();
                });
            }
        }

        private void LogPipeline(HttpRequest request, HttpResponse response)
        {
            var separator = new string('-', 50);

            var log = new StringBuilder();

            log
                .AppendLine()
                .AppendLine(separator)
                .AppendLine("REQUEST:")
                .AppendLine(request.ToString())
                .AppendLine()
                .AppendLine("RESPONSE:")
                .AppendLine(response.ToString());

            Console.WriteLine(log.ToString());
        }

        private void PrepareSession(HttpRequest request, HttpResponse response)
        {
            response.AddCookie(HttpSession.SessionCookieName, request.Session.Id);
        }

        private async Task HandleError(NetworkStream networkStream, Exception exception)
        {
            var errorMessage = $"{exception.Message}{Environment.NewLine}{exception.StackTrace}";

            var errorResponse = HttpResponse.ForError(errorMessage);

            await this.WriteResponse(networkStream, errorResponse);
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var requestBuilder = new StringBuilder();

            var totalBytesRead = 0;

            do
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);

                totalBytesRead += bytesRead;

                if (totalBytesRead > 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large.");
                }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);

            return requestBuilder.ToString();
        }

        private async Task WriteResponse(
            NetworkStream networkStream,
            HttpResponse response)
        {          
            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());

            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    }
}
