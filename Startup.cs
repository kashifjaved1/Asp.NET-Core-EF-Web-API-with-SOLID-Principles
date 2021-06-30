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
using AutoMapper;
using PaymentGatewayAPI.Models;
using Microsoft.EntityFrameworkCore;
using PaymentGatewayAPI.PaymentProvider;
using PaymentGatewayAPI.Services;
using PaymentGatewayAPI.Repositories;

namespace PaymentGatewayAPI
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
            services.AddCors(); // enabling Cross-Origin-Sharing (CORS)

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<PaymentContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Conn")));

            services.AddScoped<ICheapPaymentGateway, CheapPaymentGateway>();
            services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddScoped<IPaymentRequestService, PaymentRequestService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentStateMgmtRepository, PaymentStateMgmtRepository>();
            services.AddAutoMapper(c => {
                c.AddProfile<Services.MapperProfile.PaymentProfile>();
                c.AddProfile<Services.MapperProfile.PaymentStateProfile>();
            }, typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors( // setting-up, Cross-Origin-Sharing (CORS) Origins
                options => options.WithOrigins(
                                    "http://localhost:4200", "https://localhost:4200"
                                    )
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowCredentials()
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
