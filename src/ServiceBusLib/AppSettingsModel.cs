namespace ServiceBusApp;

public class AppSettingsModel
{
  public required string topicName { get; set; }
  public required string serviceBusNamespace { get; set; }

  // optional properties
  public string? subscriptionName { get; set; }
}
