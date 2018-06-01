using System;
using System.Collections.Generic;
using System.IO;
using Agora.Brokerage.Cadastro.DadosAcesso.Core.Factories;
using Agora.Brokerage.Cadastro.DadosAcesso.Core.Repositories;
using Agora.Brokerage.Cadastro.DadosAcesso.Core.Services;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;
using IO.Swagger.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Agora.Brokerage.Cadastro.DadosAcesso.API
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
            services.AddRouting();
            ConfigureIOC(services);
            //ConfigureSwaggerServices(services);
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Cadastro de dados de acesso", Version = "v1" });
                c.CustomSchemaIds(type => type.FriendlyId(true));
                c.DescribeAllEnumsAsStrings();
                c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}Agora.Brokerage.Cadastro.DadosAcesso.API.xml");
                c.OperationFilter<GeneratePathParamsValidationFilter>();
            });

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                //Dados acesso

                cfg.CreateMap<Models.Senha, Model.Models.Senha>();
                cfg.CreateMap< Model.Models.Senha, Models.Senha>();

                //Dados acesso

                cfg.CreateMap<Models.DadosAcesso, Model.Models.DadosAcesso>();
                cfg.CreateMap<Model.Models.DadosAcesso, Models.DadosAcesso>();

                cfg.CreateMap<Models.DadosAcessoPost, Model.Models.DadosAcesso>();
                cfg.CreateMap<Model.Models.DadosAcesso, Models.DadosAcessoPost>();

                cfg.CreateMap<Models.DadosAcessoPut, Model.Models.DadosAcesso>();
                cfg.CreateMap<Model.Models.DadosAcesso, Models.DadosAcessoPut>();

                cfg.CreateMap<Models.Assessor, Model.Models.Assessor>();
                cfg.CreateMap<Model.Models.Assessor, Models.Assessor>();

                cfg.CreateMap<Models.Senha, Model.Models.Senha>();
                cfg.CreateMap<Model.Models.Senha, Models.Senha>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void ConfigureIOC(IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory,ConexaoFactory>();
            services.AddTransient<IDadosAcessoRepositorio, DadosAcessoRepositorio>();
            services.AddTransient<IDadosAcessoServico, DadosAcessoServico>();
            services.AddTransient<IAssessoresServico, AssessoresServico>();
            services.AddTransient<IAssessoresRepositorio, AssessoresRepositorio>();
            services.AddTransient<ISenhaServico,SenhaServico>();
            services.AddTransient<ISenhaRepositorio, SenhaRepositorio>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cadastro de dados de acesso - API v1");
            });


            app.UseMvc();
        }

    }
}
