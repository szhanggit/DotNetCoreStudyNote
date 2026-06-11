using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

namespace ProtoConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create the Client
            // The port number must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("http://localhost:9018");
            var client = new TXC.Proto.Product.Product.ProductClient(channel);

            //var getProducts = await GetProducts(client);
            //var getProductId = await GetProductById(client);
            var updateProductAcceptanceLoop = await UpdateProductAcceptanceLoop(client);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
        static async Task<string> GetProducts(TXC.Proto.Product.Product.ProductClient client)
        {
            // Create a call / request
            var reply = await client.GetProductsAsync(new TXC.Proto.Product.GetProductsRequest()
            {
                Keyword = "1234"
            });

            return reply.Message;
        }
        static async Task<string> GetProductById(TXC.Proto.Product.Product.ProductClient client)
        {
            var reply = await client.GetProductByIdAsync(new TXC.Proto.Product.GetProductByIdRequest()
            {
                ProductId = 1,
                TenantId = 1,
                TenantName = "TW"
            });

            return reply.Message;
        }
        static async Task<string> UpdateProductAcceptanceLoop(TXC.Proto.Product.Product.ProductClient client)
        {
            try
            {
                var request = new TXC.Proto.Product.UpdateProductAcceptanceLoopRequest
                {
                    TenantId = 9,
                    TenantName = "GL",
                    TX2UserName = "jgao",
                    ProductId = 1,
                    AcceptanceLoopId = 0
                };
                var reply = await client.UpdateProductAcceptanceLoopAsync(request);

                return reply.Message;
            }
            catch (Exception ex) 
            {
                return ex.Message;
            }

        }
    }
}