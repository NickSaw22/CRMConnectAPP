﻿using CRMConnect.CRMConnect.Business.Implementaions;
using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Data.DataAccess;
using CRMConnect.CRMConnect.Data.Interfaces;
using CRMConnect.CRMConnect.Data.Repository;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using System.IO.Compression;
using Microsoft.Extensions.Configuration;

namespace CRMConnect.CRMConnect.Service.Extentions
{
    public static class ServiceCollectionExtensionMethods
    {
        private const int ONE_YEAR_IN_SECONDS = 31_536_000;

        /// <summary>
        /// This method will be used to add the base services that are needed for a web api to run
        /// </summary>
        /// <param name="services"></param>
        public static void AddBaseServices(this IServiceCollection services)
        {
            services.AddMvc()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddHsts(options =>
            {
                options.MaxAge = TimeSpan.FromSeconds(ONE_YEAR_IN_SECONDS);
                options.IncludeSubDomains = true;
                options.Preload = true;
            });
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddSingleton(configuration);
            services.ConfigureForwardedHeaders();

            services.AddCompression();


            services.AddHttpContextAccessor();

            services.AddHealthChecks();

            services.AddControllers().AddNewtonsoftJson();
        }

        /// <summary>
        /// This method will add the forwarded headers to the dependency services
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureForwardedHeaders(this IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options => { options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto; });
        }

        /// <summary>
        /// This method is used to add the compression related services to dependency injection
        /// </summary>
        /// <param name="services"></param>
        public static void AddCompression(this IServiceCollection services)
        {
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
                    
        }      

        
        
        /// <summary>
        /// This method will be used to add the dependencies related to api project
        /// </summary>
        /// <param name="services"></param>
        public static void AddApiServices(this IServiceCollection services)
        {       

            services.AddScoped<IAccountService, AccountService>();
        }

        /// <summary>
        /// This method will be used to add the dependencies related to Data projects
        /// </summary>
        /// <param name="services"></param>
        public static void AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            
        }


        /// <summary>
        /// This will add the leo sql client to DI
        /// </summary>
        /// <param name="services"></param>
        public static void AddSqlClient(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        /// <summary>
        /// This will add the leo nosql client to DI
        /// </summary>
        /// <param name="services"></param>
        public static void AddNoSqlClient(this IServiceCollection services)
        {
            /*services.ConfigureNoSqlClient((sp) =>
            {
                var configurationService = sp.GetRequiredService<IConfigurationService>();

                var baseUrl = configurationService.GetApplicationConfiguration(NoSqlClientConstants.LEO_DATASERVICE_BASEURL);
                var acsAppClientId = configurationService.GetApplicationConfiguration(NoSqlClientConstants.ACS_APP_CLIENTID);

                return new NoSqlClientConfig { LeoDataServiceBaseUrl = baseUrl, AcsAppClientId = acsAppClientId };
            });*/
        }
    }
}