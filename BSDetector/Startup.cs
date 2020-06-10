using System.Collections.Generic;
using System.Linq;
using BSDetector.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


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
            //context.Database.Migrate();
            context.Database.EnsureCreated();

            // seeding the DB with empty keys (having value of 0)
            var keys = new List<string>() { "lines", "smells", "files", "repos" };
            foreach (var key in keys)
            {
                var entry = context.Stats.FirstOrDefault(k => k.key == key);
                if (entry == null)
                {
                    context.Stats.Add(new Stat { key = key, value = 0 });
                    context.SaveChanges();
                }
            }

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