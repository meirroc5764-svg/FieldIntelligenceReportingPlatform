using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace ValidationProcess.ValidationConsumer;

public class Consumer
{
    private IConfiguration _kafka { get; }

    private readonly IConsumer<Null, string> _consumer;

    public Consumer(IConfiguration kafka)
    {
        _kafka = kafka;

        var config = new ConsumerConfig
        {
            BootstrapServers = _kafka["bootstrapServers"],
            GroupId = _kafka["GroupId"],
            AutoOffsetReset = AutoOffsetReset.Earliest
            
        };

        _consumer = new ConsumerBuilder<Null, string>(config).Build();

        _consumer.Subscribe(_kafka["Topic"]);
    }

    public ConsumeResult<Null, string>? readKafka()
    {
        try
        {
            var result = _consumer.Consume(TimeSpan.FromSeconds(5));

            if (result == null || result.Message.Value == null)
            {
                return null;
            }

            return result;
        }
        catch (ConsumeException ex)
        {
            Console.WriteLine($"Kafka error: {ex.Error.Reason}");
            return null;
        }
        
    }

}