using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goyada.Actions
{
    public class Authorize : ActionBase, IAction
    {
        [JsonIgnore]
        public string Method => "authorize";

        [JsonProperty("cardId")]
        public string CardId { get; set; }

        [JsonProperty("cardPassword")]
        public string CardPassword { get; set; }

        [JsonProperty("orderReference")]
        public string OrderReference { get; set; }

        [JsonProperty("localOrderDate")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd'T'HH:mm:ss.fff'Z'")]
        public DateTime? LocalOrderDate { get; set; } = null;

        [JsonProperty("authorizationAmount")]
        public int AuthorizationAmount { get; set; }

    }
}
