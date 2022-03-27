using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndApiEF.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;
using BackEndApiEF.repo;

namespace BackEndApiEF
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Repositories
            services.AddTransient(typeof(IGenericDataStore<>), typeof(GenericRepo<>));
            services.AddTransient<IBillingRepo, BillingRepo>();
            
            #endregion
            services.AddControllers()
                 .AddNewtonsoftJson(options =>options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<ProjectDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("conString"))).AddFluentValidation(s => {
                s.DisableDataAnnotationsValidation = false;
                s.ImplicitlyValidateChildProperties = true;
                s.ImplicitlyValidateRootCollectionElements = true;
                s.RegisterValidatorsFromAssemblyContaining<Startup>(); 
            });
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44351", "http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
            //services.AddCors(options => 
            //{
            //    options.AddPolicy("", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            //    ); 
            //    });
            services.Configure<FormOptions>(f => {
                f.ValueLengthLimit = int.MaxValue;
                f.MultipartBodyLengthLimit = int.MaxValue;
                f.MemoryBufferThreshold = int.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseHttpsRedirection();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"uploads")),
            //    RequestPath = new Microsoft.AspNetCore.Http.PathString("/uploads")
            //});
            app.UseCors();
             app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
