using System;
using System.Net.Http;
using System.Threading.Tasks;

using addTwoNumbersService;
using Grpc.Net.Client;

namespace gRPC_Client_Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World, We are about to explore gRPC!");

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            HttpClientHandler insecureHCH = new HttpClientHandler();
            insecureHCH.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            GrpcChannel grpcChannel = GrpcChannel.ForAddress("http://localhost:34567", new GrpcChannelOptions { HttpHandler = insecureHCH });
            AddTwoNumberService.AddTwoNumberServiceClient client = new AddTwoNumberService.AddTwoNumberServiceClient(grpcChannel);

            AddTwoNumberRequest req = new AddTwoNumberRequest { FirstNum = 320, SecondNum = 456 };
            AddTwoNumberReply reply = await client.AddTwoAsync(req);

            Console.WriteLine($"320 + 456 = {reply.ResultVal}");
        }
    }
}
