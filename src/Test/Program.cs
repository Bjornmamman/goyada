using Newtonsoft.Json;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Goyada.Client("33362", "10444867");
            
            var queryBalance = client.QueryBalance("98421738D").Result;

            nicePrint("QueryBalance", queryBalance);
            
            
            var queryCardSpend = client.QueryCardSpend("98421738D", "2176").Result;
            
            nicePrint("queryCardSpend", queryCardSpend);


            var authorize = client.Authorize(
                cardId: "98421738D",
                cardPassword: "2176",
                orderReference: "123456789",
                localOrderDate: DateTime.Now,
                authorizationAmount: 100
            ).Result;

            nicePrint("Authorize", authorize);

            if(authorize.Transaction != null)
            {
                var cancel = client.Cancel(
                    orderReference: "123456789",
                    authorizationId: authorize.AuthorizationId,
                    cancelReason: "Testing"
                ).Result;

                nicePrint("Cancel", cancel);
            }
            


            
            Console.ReadLine();
        }

        private static void nicePrint(string method, object data)
        {
            Console.WriteLine("----------------");
            Console.WriteLine(method);
            Console.WriteLine("");
            Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
            Console.WriteLine("");

            Console.ReadLine();
        }
    }
}
