using EventBus.RabbitMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ.Producer;

public class EventBusRabbitMQProducer
{
	private readonly IRabbitMQConnection _connection;

    public EventBusRabbitMQProducer()
    {
    }

    public EventBusRabbitMQProducer(IRabbitMQConnection connection)
	{
		_connection = connection;
	}

	public void PublishBasketCheckout(string queueName, BasketCheckoutEvent publishModel)
	{
		using (var channel = _connection.CreateModel())
		{
			channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
			var message = JsonConvert.SerializeObject(publishModel);
			var body = Encoding.UTF8.GetBytes(message);

			IBasicProperties properties = channel.CreateBasicProperties();
			properties.Persistent = true;
			properties.DeliveryMode = 2;

			channel.ConfirmSelect();
			channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: true, basicProperties: properties, body: body);
			channel.WaitForConfirmsOrDie();

			channel.BasicAcks += (sender, eventArgs) =>
			{
				Console.WriteLine("Sent RabbitMQ");
				//implement ack handle
				//channel.BasicAck(eventArgs.DeliveryTag, false);
			};
			channel.ConfirmSelect();
		}
	}
}
