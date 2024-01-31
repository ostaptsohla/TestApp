using Grpc.Net.ClientFactory;
using ProtoBuf.Grpc.ClientFactory;
using TestApp.Contracts.Grpc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace TestApp.Contracts;

public static class SetupGrpcClients
{
    public static IServiceCollection AddGrpcClients(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var httpClientHandler = CreateHttpHandler();

        services.AddCodeFirstGrpcClient<IProductsGrpcContract>(o =>
        {
            o.Address = new Uri("https://localhost:7267");
            o.ChannelOptionsActions.Add(opt =>
            {
                opt.MaxReceiveMessageSize = null;
                opt.MaxSendMessageSize = null;
            });
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            return httpClientHandler;
        });
        
        return services;
    }

    private static HttpClientHandler CreateHttpHandler()
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        return handler;
    }
}