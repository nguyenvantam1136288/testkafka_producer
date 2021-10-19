using Confluent.Kafka;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using testkafka.Constant;
using testkafka.RunParallel;

namespace testkafka
{
    class Program
    {
       
        static void Main(string[] args)
        {
            var pConfig = ConfigFirstValue.ProducerConfig();

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string fileNameZip = "Payawe_18102021_" + Guid.NewGuid() + ".zip"; 

            string startPath = projectDirectory + @"/ZipDirectory/Input";
            string zipPath = projectDirectory + @"/ZipDirectory/Filezip/" + fileNameZip;

            ZipFile.CreateFromDirectory(startPath, zipPath);

            //ProduceKafKa.MultiProducer(10, pConfig, 5); 

            SendData_AD.CreateTProducers(ConfigFirstValue.ProducerNumber, pConfig);
            if (true)
            {
                SendData_AD.SendFile(ConfigFirstValue.SendMessageNumber, fileNameZip);
            }
            else
            {
                //SendData_AD.Send(ConfigFirstValue.SendMessageNumber);
            }

            Console.ReadLine();
        }
    }
    public class InfoData
    {
        public string messageKey;
        public string messageVal;
    }
    public class User
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BookId { get; set; }
    }

}
