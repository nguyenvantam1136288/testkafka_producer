using Confluent.Kafka;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using testkafka.Constant;

namespace testkafka.RunParallel
{
    public class TProducer
    {
        private IProducer<string, string> Producer { get; set; }
        private IProducer<string, byte[]> ProducerFile { get; set; }
        private ConcurrentDictionary<Guid, Task<DeliveryResult<string, string>>> Tasks = new ConcurrentDictionary<Guid, Task<DeliveryResult<string, string>>>();
        public Guid Id { get; set; }
        public TProducer(ClientConfig clientConfig)
        {
            Id = Guid.NewGuid();
            Producer = new ProducerBuilder<string, string>(clientConfig).Build();
            ProducerFile = new ProducerBuilder<string, byte[]>(clientConfig).Build();
        }
        public void Send(string topic, Message<string, string> message)
        {
            var timerRecord = new Stopwatch();
            timerRecord.Start();

            //var task = Producer.ProduceAsync(topic, message); 
            //Tasks.TryAdd(Guid.NewGuid(), task);

            for (int attempts = 0; attempts < ConfigFirstValue.Retry; attempts++)
            {
                try
                {
                    Producer.Produce(topic, message);
                    break;
                }
                catch { }
                Thread.Sleep(50);
            }

            timerRecord.Stop();
            TimeSpan timeTakenRecord = timerRecord.Elapsed;
            Console.WriteLine(" Thoi gian gui 1 record: " + timeTakenRecord);
        }
        public void SendFile(string topic, Message<string, byte[]> message)
        {
            var timerRecord = new Stopwatch();
            timerRecord.Start();

            //var task = Producer.ProduceAsync(topic, message); 
            //Tasks.TryAdd(Guid.NewGuid(), task);

            for (int attempts = 0; attempts < ConfigFirstValue.Retry; attempts++)
            {
                if(message == null)
                {
                    continue;
                }
                try
                {
                    ProducerFile.Produce(topic, message);
                    break;
                }
                catch { }
                Thread.Sleep(50);
            }

            timerRecord.Stop();
            TimeSpan timeTakenRecord = timerRecord.Elapsed;
            Console.WriteLine(" Thoi gian gui 1 record: " + timeTakenRecord);
        }
        public void Close()
        {
            if (Producer != null)
                Producer.Dispose();
            Tasks.Clear();
        }
    }
}
