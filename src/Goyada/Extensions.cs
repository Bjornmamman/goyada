using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Goyada
{
    public static class Extensions
    {
        public static string GetFingerPrint(this JObject o, string secret, string type = "MD5")
        {
            var parameters = new string[] {
                "merchantId",
                "cardId",
                "cardPassword",
                "orderReference",
                "authorizationAmount",
                "authorizationId"
            };
            var properties = o.Properties();
            var fingerPrint = o["timeStamp"].ToObject<DateTime>().ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");

            foreach(var p in parameters)
            {
                var prop = properties.FirstOrDefault(x => x.Name.Equals(p, StringComparison.InvariantCultureIgnoreCase));
                if (prop != null)
                    fingerPrint += prop.Value;
            }

            //fingerPrint += string.Join("", parameters.Where(x => properties.Any(p => p..Select(x => x.Value.ToString()));
            fingerPrint += secret;
            
            return fingerPrint.GetFingerPrint(type);
        }

        public static string GetFingerPrint(this string input, string type = "MD5")
        {
            HashAlgorithm algorithm = MD5.Create();

            if(type == "SHA256")
                algorithm = SHA256.Create();
            
            string hex = "";

            foreach (byte x in algorithm.ComputeHash(Encoding.UTF8.GetBytes(input)))
                hex += String.Format("{0:x2}", x);

            return hex;
        }
    }
}
