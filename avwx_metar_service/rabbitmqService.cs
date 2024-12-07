using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class RabbitMQService
{
    private readonly string _hostname;
    private readonly int _port;
    private readonly string _queueName;

    public RabbitMQService(string hostname, int port, string queueName)
    {
        _hostname = hostname;
        _port = port;
        _queueName = queueName;
    }

    public void PublishMessage(object message)
    {
        var factory = new ConnectionFactory
        {
            HostName = _hostname,
            Port = _port
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        // Declare the queue
        channel.QueueDeclare(
            queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        // Serialize the Metar object to JSON
        var messageBody = JsonSerializer.Serialize(message, new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        var body = Encoding.UTF8.GetBytes(messageBody);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true; // Ensures messages persist if RabbitMQ restarts

        // Publish the message
        channel.BasicPublish(
            exchange: "",
            routingKey: _queueName,
            basicProperties: properties,
            body: body);

        Console.WriteLine($"Message published to RabbitMQ: {message}");
    }
}
