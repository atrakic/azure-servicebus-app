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


var client = serviceProvider.GetRequiredService<ServiceBusClient>();
var topicName = configuration["ServiceBus:TopicName"];

for (int i = 0; i < args.Length; i++)
{
    var sender = client.CreateSender(topicName);
    var message = new ServiceBusMessage($"Hello from submitter: {i + 1}");
    message.ApplicationProperties["country"] = args[i] == "us" ? "usa" : args[i];
    await sender.SendMessageAsync(message);
    System.Console.WriteLine("Message sent");
}
