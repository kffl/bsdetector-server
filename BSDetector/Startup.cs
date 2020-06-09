using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSDetector.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BSDetector
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

            services.AddDbContext<StatsContext>(o => o.UseSqlite("Data Source=stats.db"));

            services.AddCors(o => o.AddPolicy("ClientApp", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StatsContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var optionsBuilder = new DbContextOptionsBuilder<StatsContext>();
            optionsBuilder.UseSqlite("Data Source=stats.db");
            //using (var context = new StatsContext(optionsBuilder.Options))
            //{
            //context.Database.Migrate();
            context.Database.EnsureCreated();

            var k = context.Stats.FirstOrDefault(k => k.key == "lines");
            Console.WriteLine(k);
            if (k == null)
            {
                context.Stats.Add(new Stat { key = "lines", value = 0 });
                Console.WriteLine("is null");
            }

            context.SaveChanges();
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}