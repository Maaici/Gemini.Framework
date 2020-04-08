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
            #region 第三方log工具serilog的相关配置

            Log.Logger = new LoggerConfiguration()
                //配置日志最小输出的级别为：debug
                .MinimumLevel.Warning()
                //如果是Microsoft的日志，最小记录等级为info
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                //输出到控制台
                .WriteTo.Console()
                //将日志保存到文件中（两个参数分别是日志的路径和生成日志文件的频次，当前是一天一个文件）
                .WriteTo.File(Path.Combine("logs", @"log.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            #endregion

            var host = CreateHostBuilder(args).Build();

            #region 添加种子数据，也可以在dbcontext中的OnModelCreating 中添加

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
            //指定用autoFac
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureLogging(config => config.AddSerilog())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
