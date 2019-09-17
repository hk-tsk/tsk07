using DAEF;
using DAEFC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace webApi
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MigrateDbContext(configuration);
        }

        private void MigrateDbContext(IConfiguration configuration)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<LearningSiteDBContext>()
             .UseSqlServer(configuration["connectionString"])
             ;
                LearningSiteDBContext context = new LearningSiteDBContext(optionsBuilder.Options);

                context.Database.Migrate();

                LearningSiteDBContextSeed.Init(configuration["connectionString"]).Wait();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }
        public IConfiguration Configuration { get; }

        readonly string customOrgions = "_customOrgins";
        private string[] orgins = new List<string>() { "http://localhost:3000" }.ToArray();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(customOrgions, builder =>
                {
                    builder.WithOrigins(orgins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });


            services.AddDbContext<LearningSiteDBContext>(options =>
            {
                options.UseSqlServer(Configuration["connectionString"], sqlOption =>
                {
                    sqlOption.MigrationsAssembly("DAEF");
                });
            });

            services.AddTransient(typeof(LearningSiteDBContext));
            services.AddTransient(typeof(DACommon.IUnitOfWork), typeof(UnitOfWork));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(action =>
                {
                    action.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(option =>
            {
                option.WithOrigins(orgins)
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
                //.WithMethods("get");
            });

            app.UseHttpsRedirection();
            app.UseMvc();

        }
    }
}
