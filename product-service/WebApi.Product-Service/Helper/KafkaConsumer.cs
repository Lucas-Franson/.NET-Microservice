

using Confluent.Kafka;

public class KafkaConsumer : BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;

    private readonly ILogger<KafkaConsumer> _logger;
    private readonly string BOOTSTRAP_SERVERS = "localhost:9092";
    
    public KafkaConsumer(IConfiguration configuration, ILogger<KafkaConsumer> logger)
    {
        _logger = logger;

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = BOOTSTRAP_SERVERS,
            GroupId = "ProductServiceGroup",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("InventoryUpdates");

        while (!stoppingToken.IsCancellationRequested)
        {
            ProcessKafkaMessage(stoppingToken);

            Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }

        _consumer.Close();
    }

    public void ProcessKafkaMessage(CancellationToken stoppingToken)
    {
        try
        {
            var consumeResult = _consumer.Consume(stoppingToken);

            var message = consumeResult.Message.Value;

            _logger.LogInformation($"Received inventory update: {message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error processing Kafka message: {ex.Message}");
        }
    }

}