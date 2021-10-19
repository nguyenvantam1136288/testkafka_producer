using Confluent.Kafka;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace testkafka
{
    public class SendData
    {
        static string data = Data.json;
        public static void Send(IProducer<string,string> producer, string topic, int numProduced, int numMessages)
        {
            string mahoa = string.Empty;
            try
            {
                mahoa = EncryptAndDecrypt.Encrypt(data, EncryptAndDecrypt.Password);

                #region Test convert data to obejct
                var giaima = EncryptAndDecrypt.Decrypt(mahoa, EncryptAndDecrypt.Password);
                var model = JsonConvert.DeserializeObject<List<User>>(data);

                //Sau khi giai ma
                var Unicode = System.Text.ASCIIEncoding.Unicode.GetByteCount(mahoa);
                var ASCII = System.Text.ASCIIEncoding.ASCII.GetByteCount(mahoa);

                var Unicode_Size = GetSize.ToSize(Unicode, GetSize.SizeUnits.MB);
                var ASCII_Size = GetSize.ToSize(ASCII, GetSize.SizeUnits.MB);


                Console.WriteLine($"Model count: " + model.Count() + " Unicode/ToSize: " + Unicode + "/" + Unicode_Size
                    + " ASCII/ToSize: " + ASCII + "/" + ASCII_Size);
                //JObject rss = JObject.Parse(data);
                //User album = rss.ToObject<User>();
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error convert json to object");
                Console.ReadKey();
            }
           
            producer.Produce(topic, new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = mahoa },
                (deliveryReport) =>
                {
                    if (deliveryReport.Error.Code != ErrorCode.NoError)
                    {
                        Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                    }
                    else
                    {
                        //Console.WriteLine($"Produced message to: {deliveryReport.TopicPartitionOffset}");
                        numProduced += 1;
                    }
                });
        }
    }
}
