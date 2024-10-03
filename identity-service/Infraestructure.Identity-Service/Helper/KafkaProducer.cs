
using Confluent.Kafka;

public class KafkaProducer : IKafkaProducer {

    private readonly string _BOOTSTRAP_SERVERS = "localhost:9092";
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer () {
        var producerconfig = new ProducerConfig
        {
            BootstrapServers = _BOOTSTRAP_SERVERS,
            AllowAutoCreateTopics = true,
            Acks = Acks.All
        };

        _producer = new ProducerBuilder<Null, string>(producerconfig).Build();
    }

    public async Task ProduceAsync(string topic, string message)
    {
        var kafkamessage = new Message<Null, string> { Value = message };

        await _producer.ProduceAsync(topic, kafkamessage);
    }
}