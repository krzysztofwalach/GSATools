using System.IO;
using Newtonsoft.Json;

namespace GSATools.Core.Models
{
    public class GSAConnectionParams
    {
        public string Protocol { get; set; }
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string GetFullHostInfo()
        {
            return string.Format("{0}://{1}:{2}", Protocol, Hostname, Port);
        }

        public static GSAConnectionParams DeserializeFromFile(string path)
        {
            string content = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<GSAConnectionParams>(content);
        }
    }
}