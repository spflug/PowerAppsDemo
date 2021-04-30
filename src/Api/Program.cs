using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PowerApps;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(b => { b.UseStartup<Startup>(); })
    .Build()
    .Run();
