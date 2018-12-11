﻿using System;
using System.Threading.Tasks;
using CryptoWatcher.Domain.Expressions;
using Hangfire;
using CryptoWatcher.Domain.Models;
using CryptoWatcher.Shared.Domain;
using CryptoWatcher.Persistence.Contexts;
using CryptoWatcher.Shared.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace CryptoWatcher.BackgroundJobs
{
    public class SendTelgramNotifications
    {
        private readonly MainDbContext _mainDbContext;
        private readonly ILogger<SendWhatsappNotificationsJob> _logger;
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IConfiguration _configuration;

        public SendTelgramNotifications(
            MainDbContext mainDbContext,
            ILogger<SendWhatsappNotificationsJob> logger,
            IRepository<Notification> notificationRepository,
            IConfiguration configuration)
        {
            _mainDbContext = mainDbContext;
            _logger = logger;
            _notificationRepository = notificationRepository;
            _configuration = configuration;
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task Run()
        {
            try
            {
                // Get pending notifications
                var pendingNotifications = await _notificationRepository.GetAll(NotificationExpression.PendingNotification());

                // If there are pending notifications
                    if (pendingNotifications.Count > 0)
                {
                    // Connect
                    var apiToken = _configuration["TelegramApiToken"];
                    var bot = new TelegramBotClient(apiToken);
                  

                    // For each notification
                    var count = 0;
                    foreach (var pendingNotification in pendingNotifications)
                    {
                        try
                        {
                            // Send whatsapp
                            await bot.SendTextMessageAsync("@crypto_watcher_official", "Hola");
                            pendingNotification.SendWhatsapp();
                            count++;
                        }
                        catch (Exception ex)
                        {
                            // Log into Splunk
                            _logger.LogSplunkError(pendingNotification, ex);
                        }
                    }

                    // Save
                    await _mainDbContext.SaveChangesAsync();

                    // Log into Splunk
                    _logger.LogSplunkInformation(new
                    {
                        TelegramsSent = count
                    });

                    // Return
                    await Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {
                // Log into Splunk
                _logger.LogSplunkError(ex);
            }
        }
    }
}