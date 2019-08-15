using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KuroruChan.SecurityBot.Configurations;
using KuroruChan.SecurityBot.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KuroruChan.SecurityBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                //Under development mode, use long polling method to get updates
                services.AddSingleton<IHostedService, LongPollingService>();
                services.AddSingleton<IUpdateService, UpdateService>();
            }
            else
            {
                //Under production mode, use mvc for webhook
                services.AddMvc();
                services.AddScoped<IUpdateService, UpdateService>();
            }
            //Add bot client
            services.AddSingleton<IBotService, BotService>();
            services.AddSingleton<IAnonymousService, AnonymousService>();
            //Add configurations
            services.Configure<BotConfiguration>(Configuration.GetSection("BotConfiguration"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseMvc();
            }

        }
    }
}
