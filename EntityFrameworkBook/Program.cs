
using EntityFrameworkBook.Config;
using EntityFrameworkBook.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection"); 
                Console.WriteLine($"Connection String: {connectionString}"); 

                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite(connectionString)
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

                services.AddHostedService<BookReaderWorker>();
            });
}