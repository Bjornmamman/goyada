using Goyada.Actions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Goyada
{
    public class Client
    {
        private string baseUrl => "https://www.goyada.se/fuel/giftcard/{0}.fuel";
        private string merchantId = null;
        private string fingerPrintAlgorithm = "MD5";
        private string secret;

        public Client(string merchantId, string secret, bool debug = true)
        {
            this.merchantId = merchantId;
            this.secret = secret;
            
            if (!debug)
                fingerPrintAlgorithm = "SHA256";
        }

        /// <summary>
        /// Returns the current balance / available amount on specified card
        /// </summary>
        /// <param name="cardId">This is a unique ID identifying a card in the GiftCard system. Normally printed on the card as ”Kort ID”. - 8 digits (plus ”D” for digital cards or ”B” for pre-auth cards)</param>
        /// <returns></returns>
        public async Task<Goyada.Response.ResponseData> QueryBalance(string cardId)
        {
            return await Request(new QueryBalance()
            {
                CardId = cardId
            });
        }

        /// <summary>
        /// Returns the current balance / available amount on specified cardAND validates cardPassword
        /// </summary>
        /// <param name="cardId">This is a unique ID identifying a card in the GiftCard system. Normally printed on the card as ”Kort ID”. - 8 digits (plus ”D” for digital cards or ”B” for pre-auth cards)</param>
        /// <param name="cardPassword">This is a password or a ”web-code” used to identify the GiftCard holder in combination with the CardId. It is either hidden behind a ”scratch panel” on the back of the GiftCard, or printed on the receipt that is given to the GiftCard holder when purchasing card. - 4 to 15 characters, normally 4 digits</param>
        /// <returns></returns>
        public async Task<Goyada.Response.ResponseData> QueryCardSpend(string cardId, string cardPassword)
        {
            return await Request(new QueryCardSpend()
            {
                CardId = cardId,
                CardPassword = cardPassword
            });
        }
        /// <summary>
        /// Validates cardPassword and authorizes a purchase transaction on the given card registered on the given merchantId (online webshop). This method will only reserve the given amount. It requires a settle or cancel to commit or cancel the transaction. This is convenient when the total order amount of the online webshop order is higher than the available amount on the GiftCard. The excess amount can be paid with creditcard through the online webshop default payment provider. If the excess payment succeed, settle should be called, if the excess payment failes, cancel can be called and the GiftCard balance is ”un-touched”
        /// </summary>
        /// <param name="cardId">This is a unique ID identifying a card in the GiftCard system. Normally printed on the card as ”Kort ID”. - 8 digits (plus ”D” for digital cards or ”B” for pre-auth cards)</param>
        /// <param name="cardPassword">This is a password or a ”web-code” used to identify the GiftCard holder in combination with the CardId. It is either hidden behind a ”scratch panel” on the back of the GiftCard, or printed on the receipt that is given to the GiftCard holder when purchasing card. - 4 to 15 characters, normally 4 digits</param>
        /// <param name="orderReference">This is an orderId or reference identifying the purchase at the Partner’s online web-shop. Given to Goyada’s GiftCard system when making a payment.</param>
        /// <param name="localOrderDate"> The local order date at the Partner’s online web-shop.</param>
        /// <param name="authorizationAmount">The requested payment amount in LMU (öre) from the Partner’s online web-shop.</param>
        /// <returns></returns>
        public async Task<Goyada.Response.ResponseData> Authorize(string cardId, string cardPassword, string orderReference, DateTime? localOrderDate, int authorizationAmount)
        {
            return await Request(new Authorize()
            {
                CardId = cardId,
                CardPassword = cardPassword,
                OrderReference = orderReference,
                LocalOrderDate = localOrderDate,
                AuthorizationAmount = authorizationAmount
            });
        }

        /// <summary>
        ///  Cancels a given authorize transaction. Makes the transaction amount available again on the card. Transaction is marked with the specified cancelReason (a text of maximum 100 chars given by the Partner)
        /// </summary>
        /// <param name="orderReference">This is an orderId or reference identifying the purchase at the Partner’s online web-shop. Given to Goyada’s GiftCard system when making a payment.</param>
        /// <param name="authorizationId">This is a unique ID identifying the payment in the GiftCard system. It can be used in any contact with Goyada to identify a payment.</param>
        /// <param name="cancelReason">A text of maximum 100 chars</param>
        /// <returns></returns>
        public async Task<Goyada.Response.ResponseData> Cancel(string orderReference, string authorizationId, string cancelReason)
        {
            return await Request(new Cancel()
            {
                OrderReference = orderReference,
                AuthorizationId = authorizationId,
                CancelReason = cancelReason
            });
        }

        /// <summary>
        /// Settles a given authorize transaction i.e ”dagsavslut”. This will result in a settlement payment to the given merchant (online webshop) to an agreed bank-account
        /// </summary>
        /// <param name="orderReference">This is an orderId or reference identifying the purchase at the Partner’s online web-shop. Given to Goyada’s GiftCard system when making a payment.</param>
        /// <param name="authorizationId">This is a unique ID identifying the payment in the GiftCard system. It can be used in any contact with Goyada to identify a payment.</param>
        /// <returns></returns>
        public async Task<Goyada.Response.ResponseData> Settle(string orderReference, string authorizationId)
        {
            return await Request(new Settle()
            {
                OrderReference = orderReference,
                AuthorizationId = authorizationId
            });
        }

        /// <summary>
        /// Exact same method as settle but with a better name for the purpose. Transaction actually gets CLEARED by the method settle (and then SETTLED at the actual disburse to the Merchant).
        /// </summary>
        /// <param name="orderReference">This is an orderId or reference identifying the purchase at the Partner’s online web-shop. Given to Goyada’s GiftCard system when making a payment.</param>
        /// <param name="authorizationId">This is a unique ID identifying the payment in the GiftCard system. It can be used in any contact with Goyada to identify a payment.</param>
        /// <param name="clearingAmount">If clearingAmount is present and less than authorizationAmount , the transaction’s amount is adjusted to new clearingAmount and card is reimbursed with authorizationAmount minus clearingAmount</param>
        /// <returns></returns>
        public async Task<Goyada.Response.ResponseData> Clear(string orderReference, string authorizationId, int? clearingAmount)
        {
            return await Request(new Clear()
            {
                OrderReference = orderReference,
                AuthorizationId = authorizationId,
                ClearingAmount = clearingAmount
            });
        }

        /// <summary>
        ///  Will find the given transaction (authorizationId) and return the status of the transaction
        /// </summary>
        /// <param name="orderReference">This is an orderId or reference identifying the purchase at the Partner’s online web-shop. Given to Goyada’s GiftCard system when making a payment.</param>
        /// <param name="authorizationId">This is a unique ID identifying the payment in the GiftCard system. It can be used in any contact with Goyada to identify a payment.</param>
        /// <returns></returns>
        public async Task<Goyada.Response.ResponseData> Status(string orderReference, string authorizationId)
        {
            return await Request(new Status()
            {
                OrderReference = orderReference,
                AuthorizationId = authorizationId
            });
        }


        private async Task<Goyada.Response.ResponseData> Request<T>(T action) where T : class, IAction 
        {
            action.MerchantId = merchantId;
            action.TimeStamp = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));

            var jsonObject = JObject.FromObject(action);
            jsonObject["fingerPrint"] = jsonObject.GetFingerPrint(secret, type: fingerPrintAlgorithm);
            jsonObject["timeStamp"] = action.TimeStamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");

            var url = string.Format(baseUrl, action.Method);
            var jsonString = jsonObject.ToString();
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(url, httpContent);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<Response.ResponseData>(content);
                }

                throw new Exception($"{response.StatusCode} - {response.Content}");
            }
            
        }
    }
}
