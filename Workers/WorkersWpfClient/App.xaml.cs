using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using WorkersWpfClient.Interface;
using WorkersWpfClient.Models.MappingProfiles;
using WorkersWpfClient.Services;
using WorkersWpfClient.ViewModels;

namespace WorkersWpfClient
{
    public partial class App : Application
    {
        private static IHost _hosting;

        public App()
        {
            _hosting = Host
                .CreateDefaultBuilder(Environment.GetCommandLineArgs())
                .ConfigureServices(ConfigureServices)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile("settings.json").AddEnvironmentVariables();
                })
                .Build();
        }

        public static T GetService<T>() => _hosting.Services.GetRequiredService<T>();

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddLogging(
                builder =>
                {
                    builder.ClearProviders();
                    builder.SetMinimumLevel(LogLevel.Information);
                    builder.AddNLog(host.Configuration);
                });

            // Services
            services.AddAutoMapper(typeof(WorkerProfile));
            services.AddSingleton<IWorkerService, WorkerService>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
        }
    }
}
