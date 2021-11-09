using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using APIAnnouncements.Context;
using APIAnnouncements.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using System.IO;
using APIAnnouncements.Options;
using APIAnnouncements.Services.RecaptchaService;

namespace APIAnnouncements
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AnnouncementsContext>(options => options.UseNpgsql(connection));
            services.AddControllers();
            services.AddTransient<IAnnouncService, AnnouncingService>();
            services.AddTransient<IUserService, UserService>();
            services.Configure<MaxAnnouncCountOption>(Configuration);
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "APIAnnouncements"
            }));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.Configure<ReCaptchaOptions>(Configuration.GetSection("ReCaptcha"));
            //services.AddHttpClient<IRecaptchaService, GoogleRecaptchaService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIAnnouncements V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @Configuration["BigPicturesDirectory"])),
                RequestPath = new Microsoft.AspNetCore.Http.PathString("/BigPictures")
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @Configuration["SmallPicturesDirectory"])),
                RequestPath = new Microsoft.AspNetCore.Http.PathString("/SmallPictures")
            });
        }
    }
}
