using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goyada.Actions
{
    public class QueryBalance : ActionBase, IAction
    {
        [JsonIgnore]
        public string Method => "querybalance";

        [JsonProperty("cardId")]
        public string CardId { get; set; }

    }
}
