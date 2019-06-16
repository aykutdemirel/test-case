using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using TestCase.Config;
using TestCase.Consumers;
using TestCase.Models;
using TestCase.Repositories;

namespace TestCase
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
            var config = new ServerConfig();
            Configuration.Bind(config);

            var newsContext = new NewsContext(config.MongoDB);
            var repo = new NewsRepository(newsContext);

            services.AddSingleton<INewsRepository>(repo);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddElasticsearch(Configuration);

            services.AddMassTransit(x =>
            {
                x.AddConsumer<MongoConsumer>();
                x.AddConsumer<ElasticSearchConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri(RabbitMQConfig.RabbitMQUri), hst =>
                    {
                        hst.Username(RabbitMQConfig.RabbitMQUserName);
                        hst.Password(RabbitMQConfig.RabbitMQPassword);
                    });

                    cfg.ReceiveEndpoint(host, "demiroren.news", ep =>
                    {
                        ep.ConfigureConsumer<MongoConsumer>(provider);
                        ep.ConfigureConsumer<ElasticSearchConsumer>(provider);
                    });
                }));
            });

            services.AddSingleton<IHostedService, MassTransitConsoleHostedService>();

            //var bus = BusConfig.Instance
            //    .ConfigureBus((cfg, host) =>
            //    {
            //        cfg.ReceiveEndpoint(host, "demiroren.news", e =>
            //        {
            //            // e.Consumer<NewsConsumer>();
            //            e.LoadFrom();
            //        });
            //    });

            //bus.Start();

            // swagger
            services.AddSwaggerGen(n =>
            {
                n.SwaggerDoc("v1", new Info
                {
                    Title = "News API",
                    Version = "v1",
                    Description = "News API using MongoDB & Elastic Search & RabbitMQ",
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(n =>
            {
                n.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}