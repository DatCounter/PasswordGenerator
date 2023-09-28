using PasswordGenerator;

IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices(services => { services.AddHostedService<PasswordGeneratorWorker>(); })
                 .Build();

host.Run();
