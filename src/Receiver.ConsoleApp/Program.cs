using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfiguration>(configuration)
    .AddSingleton<ServiceBusClient>(provider =>
    {
        var config = provider.GetRequiredService<IConfiguration>();
        var serviceBusNamespace = config["ServiceBus:Namespace"];
        var credential = new DefaultAzureCredential();
        return new ServiceBusClient(
            fullyQualifiedNamespace: $"{serviceBusNamespace}.servicebus.windows.net",
            credential: credential);
    })
    .BuildServiceProvider();


var options = new ServiceBusReceiverOptions
{
    ReceiveMode = ServiceBusReceiveMode.PeekLock
};

var client = serviceProvider.GetRequiredService<ServiceBusClient>();
var topicName = configuration["ServiceBus:TopicName"];
var subscriptionName = configuration["ServiceBus:SubscriptionName"];

var receiver = client.CreateReceiver(
    topicName: topicName,
    subscriptionName: subscriptionName,
    options: options);

var response = await receiver.ReceiveMessagesAsync(maxMessages: 5);
while (response.Count > 0)
{
    foreach (var message in response)
    {
        var content = message.Body.ToString();
        System.Console.WriteLine($"Received Message: {content}");
    };
    response = await receiver.ReceiveMessagesAsync(maxMessages: 5);
}
