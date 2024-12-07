using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly ServiceBusClient _client;

    public Worker(ILogger<Worker> logger, IConfiguration configuration, ServiceBusClient client)
    {
        _logger = logger;
        _configuration = configuration;
        _client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var options = new ServiceBusReceiverOptions
        {
            ReceiveMode = ServiceBusReceiveMode.PeekLock
        };

        var topicName = _configuration["ServiceBus:TopicName"];
        var subscriptionName = _configuration["ServiceBus:SubscriptionName"];

        var receiver = _client.CreateReceiver(
            topicName: topicName,
            subscriptionName: subscriptionName,
            options: options);

        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await receiver.ReceiveMessagesAsync(maxMessages: 5, cancellationToken: stoppingToken);
            while (response.Count > 0)
            {
                foreach (var message in response)
                {
                    var content = message.Body.ToString();
                    _logger.LogInformation($"Received Message: {content}");
                }
                response = await receiver.ReceiveMessagesAsync(maxMessages: 5, cancellationToken: stoppingToken);
            }
            await Task.Delay(1000, stoppingToken); // Add a delay to avoid tight loop
        }
    }
}
