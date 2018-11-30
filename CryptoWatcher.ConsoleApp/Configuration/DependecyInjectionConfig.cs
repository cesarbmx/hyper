﻿using CoinMarketCap;
using CoinMarketCap.Core;
using CryptoWatcher.BackgroundJobs;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Domain.Repositories;
using CryptoWatcher.Domain.Services;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoWatcher.ConsoleApp.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IPinnacleTokenService<CryptoWatcherPermission>, PinnacleTokenService<CryptoWatcherPermission>>();

            //Contexts (UOW)
            //services.AddDbContext<MainDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("CryptoWatcher")));
            services.AddDbContext<MainDbContext>(options => options.UseInMemoryDatabase("CryptoWatcher"));

            // Services
            services.AddSingleton<CacheService, CacheService>();
            services.AddSingleton<StatusService, StatusService>();
            services.AddSingleton<ErrorMessagesService, ErrorMessagesService>();
            services.AddSingleton<UserService, UserService>();
            services.AddSingleton<OrderService, OrderService>();

            // Repositories
            services.AddScoped<IRepository<Cache>, Repository<Cache>>();
            services.AddScoped<IRepository<Log>, Repository<Log>>();
            services.AddScoped<IRepository<Watcher>, Repository<Watcher>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Notification>, Repository<Notification>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();

            // Jobs
            services.AddSingleton<ImportCurrenciesJob, ImportCurrenciesJob>();
            services.AddSingleton<MonitorWatchersJob, MonitorWatchersJob>();
            services.AddSingleton<SendWhatsappNotificationsJob, SendWhatsappNotificationsJob>();

            // Other
            services.AddSingleton<ICoinMarketCapClient, CoinMarketCapClient>();

            return services;
        }
    }
}
