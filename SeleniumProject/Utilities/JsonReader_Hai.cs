using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace SeleniumProject.Utilities
{
    public class JsonReader_Hai
    {
        private static JObject data;

        static JsonReader_Hai()
        {
            string json = File.ReadAllText("TestData/users.json");
            data = JObject.Parse(json);
        }

        public static string GetUsername(string type)
        {
            return data[type]["username"].ToString();
        }

        public static string GetPassword(string type)
        {
            return data[type]["password"].ToString();
        }
    }
}