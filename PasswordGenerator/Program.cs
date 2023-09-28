using PasswordGenerator;

IHost host = CreateHostBuilder(args)
   .Build();

host.Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
         {
             services.AddHostedService<PasswordGeneratorWorker>();
         })
        .UseWindowsService(); // Добавьте эту строку для поддержки службы Windows
