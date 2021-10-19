using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace testkafka.Constant
{
    public class ConfigFirstValue
    {
        public static string ServerKafka = "34.223.53.19:9092";

        public static ProducerConfig ProducerConfig()
        {
            var pConfig = new ProducerConfig
            {
                BootstrapServers = ConfigFirstValue.ServerKafka,
                ClientId = Dns.GetHostName(),
                Acks = Acks.All
            };
            return pConfig;
        }

        public static int ProducerNumber = 1;
        public static int SendMessageNumber = 1;
        public static string Topic = "testTopic";//testTopic topic10Part topic20Part
        public static int Retry = 3;
    }
}
