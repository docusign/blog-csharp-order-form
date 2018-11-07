using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace eg_01_csharp_jwt
{
    /// <summary>
    /// This class implement common and general implementation.
    /// </summary>
    internal class DSHelper
    {
        /// <summary>
        /// This method read bytes content from resource
        /// </summary>
        /// <param name="fileName">resource path</param>
        /// <returns>return bytes array</returns>
        internal static byte[] ReadContent(string fileName)
        {
            byte[] buff = null;
            var assembly = Assembly.GetExecutingAssembly();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    long numBytes = new FileInfo(path).Length;
                    buff = br.ReadBytes((int)numBytes);
                }
            }

            return buff;
        }
        /// <summary>
        /// This method check if directory exists and if not it create it.
        /// </summary>
        /// <param name="dirName">directory path</param>
        /// <returns>returns absolute path</returns>
        internal static string EnsureDirExistance(string dirName)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dirName);

            if (!Directory.Exists(dirName))
            {
                dirInfo = Directory.CreateDirectory(dirName);
            }

            return dirInfo.FullName;
        }
        /// <summary>
        /// This method printing pretty json format
        /// </summary>
        /// <param name="obj">any object to be written as string</param>
        internal static void PrintPrettyJSON(Object obj)
        {
            Console.WriteLine("Result:");
            string json = JsonConvert.SerializeObject(obj);
            string jsonFormatted = JValue.Parse(json).ToString(Formatting.Indented);
            Console.WriteLine(jsonFormatted);
        }
        /// <summary>
        /// This method to write byte array to file
        /// </summary>
        /// <param name="filePath">path to file</param>
        /// <param name="stream"> memory stream to write</param>
        internal static void WriteStreamToFile(string filePath, Stream stream)
        {
            using (FileStream fs = File.Create(filePath, (int)stream.Length))
            {
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, bytesInStream.Length);
                fs.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }
    }
}
