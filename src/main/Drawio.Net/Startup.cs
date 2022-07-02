using Autofac;
using AutoMapper;
using Drawio.Net.Modules;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Drawio.Net
{
    public class Startup
    {
        public Startup(IConfiguration configuration,IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddAutoMapper(Assembly.Load("Drawio.Net.Service"));

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                //v1 �������ҲҪͳһ��Сд
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, "Drawio.Net.xml");//TopucApiTpl.xml����ڽ���������ɵ�ʱ��xml�����ĵ�
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGen(sw =>
            {
                sw.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = null;
            });

            //�����ļ�ϵͳ����Կ�洢�⣨�־��Ա�����Կ��
            services.AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo($@"{WebHostEnvironment.ContentRootPath}login-keys"));

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // ֱ����Autofacע�������Զ���� 
            builder.RegisterModule(new CustomAutofacModule(builder));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

#if !DEBUG
            app.UseStaticFiles(new StaticFileOptions

            {

                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "webapp")),

                //�������·���������ǰ��������һ���ģ���ȻҲ�������ģ�ע��ǰ��Ҫ��/��

                RequestPath =new PathString("/drawui"),
                
                
            });
#endif

            app.UseRouting();

            //�����֤����֤��Ҫ��Map��������ǰд��������Ч��
            app.UseAuthentication();
            //��Ȩ
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>

            {

                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

                //Ҫ��Ӧ�õĸ�(http://localhost:<port>/) ���ṩ Swagger UI���뽫 RoutePrefix ��������Ϊ���ַ�����

            });

            //֧�ֿ�������
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();

            });

            _ = app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
