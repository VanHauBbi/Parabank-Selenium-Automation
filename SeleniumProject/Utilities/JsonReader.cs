using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.Utilities
{
    public class JsonReader
    {
        public static string GetUsername()
        {
            var path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "TestData",
                "users.json"
            );

            var json = File.ReadAllText(path);
            var data = JObject.Parse(json);

            return data["validUser"]["username"].ToString();
        }

        public static string GetPassword()
        {
            var path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "TestData",
                "users.json"
            );

            var json = File.ReadAllText(path);
            var data = JObject.Parse(json);

            return data["validUser"]["password"].ToString();
        }
    }
}
