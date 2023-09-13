using Confluent.Kafka;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "000.000.00.0000:00000", // Endereço e porta do servidor Kafka
        };

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            string topicName = "kafka_linux"; // Substitua pelo nome do seu tópico Kafka
            for (var i = 0; i < 100; i++){
                 try
                 {
                     // Produza uma mensagem
                     var message = new Message<Null, string>
                     {
                         Value = "Hello World! " + i,
                     };

                     DeliveryResult<Null, string> result = await producer.ProduceAsync(topicName, message);

                     Console.WriteLine($"Mensagem entregue para o tópico '{result.Topic}', partição {result.Partition}, offset {result.Offset}");
                 }
                 catch (ProduceException<Null, string> e)
                 {
                     Console.WriteLine($"Erro ao produzir mensagem: {e.Error.Reason}");
                 }
            }
        }
    }
}