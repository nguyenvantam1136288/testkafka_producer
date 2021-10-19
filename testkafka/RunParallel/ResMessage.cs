using Aspose.Zip;
using Aspose.Zip.Saving;
using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace testkafka.RunParallel
{
    public class ResMessage
    {
        static string data = Data.json;
        static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        static string strMessage()
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

                //Console.WriteLine($"Tam_Model count: " + model.Count() + " Unicode/ToSize: " + Unicode + "/" + Unicode_Size
                //   + " ASCII/ToSize: " + ASCII + "/" + ASCII_Size + "-" + DateTime.Now);

                //JObject rss = JObject.Parse(data);
                //User album = rss.ToObject<User>();
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error convert json to object");
                Console.ReadKey();
            }
            return mahoa;
        }
        public static Message<string, string> Mess()
        {
            var value = strMessage();
            var key = Guid.NewGuid().ToString();
            return new Message<string, string> { Key = "tam", Value = value };
        }

        public static Message<string, byte[]> MessFile(string fileNameZip)
        {
            byte[] byteFile = strMessageFile(fileNameZip);
            var key = Guid.NewGuid().ToString();
            return new Message<string, byte[]> { Key = fileNameZip, Value = byteFile };
        }
        static byte[] strMessageFile(string fileNameZip)
        {
            //https://www.codeproject.com/Articles/24017/File-Transfer-using-Socket-Application-in-C-NET-2
            string pathFileZip = projectDirectory + @"/ZipDirectory/Filezip/" + fileNameZip;
            try
            {
                string filePath = "";
                pathFileZip = pathFileZip.Replace("\\", "/");
                while (pathFileZip.IndexOf("/") > -1)
                {
                    filePath += pathFileZip.Substring(0, pathFileZip.IndexOf("/") + 1);
                    pathFileZip = pathFileZip.Substring(pathFileZip.IndexOf("/") + 1);
                }
                byte[] fileNameByte = Encoding.ASCII.GetBytes(pathFileZip);
                if (fileNameByte.Length > 850 * 1024)
                {
                    //curMsg = "File size is more than 850kb, please try with small file.";
                }
                byte[] fileData = File.ReadAllBytes(filePath + pathFileZip);
                return fileData;
            }
            catch (Exception ex)
            {
               
            }
            return null;
        }

    }
}
