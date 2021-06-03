using System;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using MyWebServer.Server.Http;

namespace MyWebServer.Server
{
    public class MyHttpServer
    {
        private readonly TcpListener listener;

        public MyHttpServer(string ipAddress, int port)
        {
            this.listener = new TcpListener(
                IPAddress.Parse(ipAddress), port);
        }

        public async Task Start()
        {
            this.listener.Start();

            Console.WriteLine($"Server is listening...");
            
            while (true)
            {
                var connection = await this.listener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var requestText = await this.ReadRequest(networkStream);
                Console.WriteLine(requestText);
                
                var request = HttpRequest.Parse(requestText);

                await this.WriteResponse(networkStream);

                connection.Close();
            }
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var requestBuilder = new StringBuilder();

            var totalBytesRead = 0;

            while (networkStream.DataAvailable)
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);

                totalBytesRead += bytesRead;

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }

            return requestBuilder.ToString();
        }

        private async Task WriteResponse(NetworkStream networkStream)
        {
            var content = "Hi от Християн!";
            var contentLength = Encoding.UTF8.GetByteCount(content);

            var response = $@"HTTP/1.1 200 OK
Date: {DateTime.UtcNow:r}
Content-Length: {contentLength}
Content-Type: text/plain; charset=UTF-8

{content}";

            var responseBytes = Encoding.UTF8.GetBytes(response);

            await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    }
}
