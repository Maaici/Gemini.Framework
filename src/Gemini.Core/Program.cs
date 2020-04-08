using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;

namespace Gemini.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region ������log����serilog���������

            Log.Logger = new LoggerConfiguration()
                //������־��С����ļ���Ϊ��debug
                .MinimumLevel.Warning()
                //�����Microsoft����־����С��¼�ȼ�Ϊinfo
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                //���������̨
                .WriteTo.Console()
                //����־���浽�ļ��У����������ֱ�����־��·����������־�ļ���Ƶ�Σ���ǰ��һ��һ���ļ���
                .WriteTo.File(Path.Combine("logs", @"log.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            #endregion

            var host = CreateHostBuilder(args).Build();

            #region ����������ݣ�Ҳ������dbcontext�е�OnModelCreating �����

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    try
            //    {
            //        var context = services.GetRequiredService<MyDbContext>();
            //        DbInitializer.Initialize(context);
            //    }
            //    catch (Exception ex)
            //    {
            //        var logger = services.GetRequiredService<ILogger<Program>>();
            //        logger.LogError(ex, "An error occurred while seeding the database.");
            //    }
            //}

            #endregion

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //ָ����autoFac
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureLogging(config => config.AddSerilog())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
