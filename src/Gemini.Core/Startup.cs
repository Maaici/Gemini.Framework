using Autofac;
using DomainModels.context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Gemini.Core
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>(options=>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Test_DB"),b=>b.MigrationsAssembly("Gemini.Core"));
            });
        }

        //autoFac容器
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //业务逻辑层所在程序集命名空间
            Assembly service = Assembly.Load("Services");
            //接口层所在程序集命名空间 
            Assembly iservice = Assembly.Load("IServices");
            //自动注入
            builder.RegisterAssemblyTypes(service, iservice)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
            //注册仓储，所有IRepository接口到Repository的映射
            //builder.RegisterGeneric(typeof(Repository<>))
            //    //InstancePerDependency：默认模式，每次调用，都会重新实例化对象；每次请求都创建一个新的对象；
            //    .As(typeof(IRepository<>)).InstancePerDependency();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
