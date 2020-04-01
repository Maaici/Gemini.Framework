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
            //������log����serilog���������
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

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(config => config.AddSerilog())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
