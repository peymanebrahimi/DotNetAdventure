using System;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MassTransitKevinSmith.Payments
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var hostBuilder =
            await Host
                .CreateDefaultBuilder(args)
                .ConfigureServices((ctx, services) =>
                {
                    services.AddMassTransit(c =>
                    {
                        c.AddConsumer<OrderRequestedConsumer>();

                        c.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("localhost", "/", h => { });

                            cfg.ReceiveEndpoint("Payments",
                                e =>
                                {
                                    e.PrefetchCount = 16;
                                    e.UseMessageRetry(r => r.Interval(2, TimeSpan.FromSeconds(10)));
                                    e.ConfigureConsumer<OrderRequestedConsumer>(context);
                                });
                        });
                    });
                    services.AddHostedService<MassTransitConsoleHostedService>();
                    services.AddHostedService<PaymentWork>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                }).RunConsoleAsync();


            //var host = hostBuilder.Build();

            //Console.WriteLine("Welcome to Payments");
            ////Console.WriteLine("Press Q key to exit");
            ////while (Console.ReadKey(true).Key != ConsoleKey.Q) ;



            //await host.RunAsync();
        }
    }
}
