using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(
    queue: "notifications",
    durable: false,
    exclusive: false,
    autoDelete: false
);

var message = "Nouvelle commande !";
var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(
    exchange: "",
    routingKey: "notifications",
    body: body
);

Console.WriteLine($"Message envoyé : {message}");