using System;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Swashbuckle.AspNetCore.Swagger;
using TestCase.Config;
using TestCase.Consumers;
using TestCase.Context;
using TestCase.Repositories;

namespace TestCase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string SpecificOrigins = "_reactiveWebPrograminCors";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new ServerConfig();
            Configuration.Bind(config);

            var newsContext = new NewsContext(config.MongoDB);
            var newsRepo = new NewsRepository(newsContext);
            var categoriesContext = new CategoriesContext(config.MongoDB);
            var categoriesRepo = new CategoriesRepository(categoriesContext);
            var imagesContext = new ImagesContext(config.MongoDB);
            var imagesRepo = new ImagesRepository(imagesContext);

            services.AddSingleton<INewsRepository>(newsRepo);
            services.AddSingleton<ICategoriesRepository>(categoriesRepo);
            services.AddSingleton<IImagesRepository>(imagesRepo);

            services.AddCors(options =>
            {
                options.AddPolicy(SpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost",
                                        "http://localhost:3000").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
            });

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

            app.UseCors(SpecificOrigins);

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(n =>
            {
                n.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseStaticFiles();



        }
    }
}