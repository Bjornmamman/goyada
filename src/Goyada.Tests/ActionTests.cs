using Goyada.Actions;
using Newtonsoft.Json.Linq;
using System;
using Xunit;

namespace Goyada.Tests
{
    public class ActionTests
    {
        private DateTime timeStamp = new DateTime(2018, 1, 1, 1, 0, 0);

        [Fact]
        public void QueryBalance()
        {
            var action = new QueryBalance()
            {
                CardId = "98421738D"
            };
             
            Assert.Equal("98421738D", action.CardId);
            Assert.Equal("querybalance", action.Method);

            var md5Fingerprint = getJson(action, "MD5");

            Assert.Equal("8f8524797aa93008ef7ec1f3f673c1c7", md5Fingerprint["fingerPrint"]);

            var sha256Fingerprint = getJson(action, "SHA256");

            Assert.Equal("defdfad236475d7dceffcbb38ffa5ca03a58a5a644fe1da4bb68dbfc58d5fc99", sha256Fingerprint["fingerPrint"]);
            
        }


        private JObject getJson(IAction action, string hashType)
        {
            action.MerchantId = "0";
            action.TimeStamp = timeStamp;

            var jsonObject = JObject.FromObject(action);
            jsonObject["fingerPrint"] = jsonObject.GetFingerPrint("NOSECRET", type: hashType);
            jsonObject["timeStamp"] = action.TimeStamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");

            return jsonObject;
        }
    }
}
