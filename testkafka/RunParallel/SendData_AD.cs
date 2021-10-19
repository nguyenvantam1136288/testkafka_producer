using Confluent.Kafka;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testkafka.Constant;

namespace testkafka.RunParallel
{
    public class SendData_AD
    {
        private static readonly ConcurrentDictionary<int, TProducer> TProducers = new ConcurrentDictionary<int, TProducer>();
        private static int ProducerCount { get; set; }
        private static readonly ConcurrentDictionary<Guid, Task> Tasks = new ConcurrentDictionary<Guid, Task>();
        public static void CreateTProducers(int number, ClientConfig clientConfig)
        {
            ProducerCount = number;
            for (int i = 0; i < number; i++)
            {
                TProducers[i] = new TProducer(clientConfig);
            }
        }
        public static void Send(int messageCount)
        {
            for (int i = 0; i < ProducerCount; i++)
            {
                int ii = i;
                Tasks[Guid.NewGuid()] = Task.Run(() =>
                {
                    int iii = ii;
                    var producer = TProducers[iii];
                    for (int k = 0; k < messageCount; k++)
                    {
                        try
                        {
                            producer.Send(ConfigFirstValue.Topic, ResMessage.Mess());
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Khong gui duoc du lieu");
                        }
                    }
                    //Console.WriteLine("Complete send " + messageCount + " of producer " + producer.Id.ToString());
                });
            }
            Console.WriteLine("Producer: " + ProducerCount + " messageCount moi pro: " + messageCount);
          
        }

        public static void SendFile(int messageCount, string fileNameZip)
        {
            for (int i = 0; i < ProducerCount; i++)
            {
                int ii = i;
                Tasks[Guid.NewGuid()] = Task.Run(() =>
                {
                    int iii = ii;
                    var producer = TProducers[iii];
                    for (int k = 0; k < messageCount; k++)
                    {
                        try
                        {
                            producer.SendFile(ConfigFirstValue.Topic, ResMessage.MessFile(fileNameZip));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Khong gui duoc du lieu");
                        }
                    }
                    //Console.WriteLine("Complete send " + messageCount + " of producer " + producer.Id.ToString());
                });
            }
            Console.WriteLine("Producer: " + ProducerCount + " messageCount moi pro: " + messageCount);

        }

        public static void Close()
        {
            Tasks.Clear();
        }
    }
}
