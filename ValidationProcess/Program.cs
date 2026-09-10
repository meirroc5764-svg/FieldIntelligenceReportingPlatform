using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using validationProcess.ConectElastic;
using ValidationProcess.ValidationConsumer;
using ValidationProcess.validationProcess;

namespace ValidationProcess.RuningProgram;

public class Program
{
    static async Task Main()
    {
        var kafka = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            .GetSection("kafka");

        var consumer = new Consumer(kafka);

        var client = new ElasticMappingConect();

        var validation = new ValidationMyReport();

        await client.CreateMappingAsync();

        int count = 0;
        int count2 = 0;
        while(true)
        {
            var message = consumer.readKafka();

            

            if (message == null)
            {
                Console.WriteLine(count);
                Console.WriteLine(count2);
                Console.WriteLine("finish read kafka");
                break;
            }

            var validmessage = validation.ValidationExec(message);

            if(validmessage !=  null)
            {
                await client.Send(validmessage);
                count2++;
            }

            count++;
        }
    }
}
