using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goyada.Response
{
    public class Transaction
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("state.name")]
        public Constants.TransactionStateName StateName { get; set; }

        [JsonProperty("state.description")]
        public string StateDescription { get; set; }

        [JsonProperty("type")]
        public Constants.TransactionType Type { get; set; }

        [JsonProperty("transactionDate")]
        public DateTime TransactionDate { get; set; }

        [JsonProperty("settled")]
        public DateTime? SettledDate { get; set; }

        [JsonProperty("cancelled")]
        public DateTime? CancelledDate { get; set; }

        [JsonProperty("cleared")]
        public DateTime? ClearedDate { get; set; }
    }
}
