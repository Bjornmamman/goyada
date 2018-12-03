using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goyada.Response
{
    public class Card
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("balance")]
        public int Balance { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("state.description")]
        public string StateDescription { get; set; }

        [JsonProperty("state")]
        public Constants.CardState State { get; set; }

        [JsonProperty("activationDate")]
        public DateTime ActivationDate { get; set; }

        [JsonProperty("expiryDate")]
        public DateTime? ExpiryDate { get; set; }

        [JsonProperty("blockedDate")]
        public DateTime? BlockedDate { get; set; }
    }
}
