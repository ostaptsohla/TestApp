using ProtoBuf.Grpc.Server;
using TestApp.GrpcServices;

namespace TestApp;

public static class SetupGrpcServer
{
    public static WebApplicationBuilder AddGrpcServer(this WebApplicationBuilder builder)
    {
        builder.Services.AddCodeFirstGrpc(options =>
        {
            options.MaxReceiveMessageSize = null;
            options.MaxSendMessageSize = null;
            
        });

        builder.Services.AddGrpc();

        return builder;
    }

    public static IEndpointRouteBuilder RegisterGrpcServices(this IEndpointRouteBuilder builder)
    {
        using (var scope = builder.ServiceProvider.CreateScope())
        {
            var webHostEnvironment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            if (webHostEnvironment.IsDevelopment())
            {
                builder.MapCodeFirstGrpcReflectionService();
            }
        }

        builder.MapGrpcService<ProductGrpcService>();

        return builder;
    }
}