using Confluent.Kafka;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testkafka
{
    public class ProduceKafKa
    {
        static string topic = "testTopic";
        public static void MultiProducer(int pro, ClientConfig config, int numMessages)
        {
            for (int i = 0; i<pro; i++)
            {
                Produce(topic, config, pro, i, numMessages);// chạy lần lượt
            }
        }
        static void Produce(string topic, ClientConfig config, int numProduced, int numStart, int numMessages)
        {
            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                for (int i = 0; i < numMessages; ++i)
                {
                    SendData.Send(producer, topic, numProduced, numMessages);
                }
                producer.Flush(TimeSpan.FromSeconds(10));
                Console.WriteLine($"So luong producer " + numProduced +"/" + numStart + ", So luong recode cho moi producer(2 json): " + numMessages);

                //Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
            }
        }
    }
}
