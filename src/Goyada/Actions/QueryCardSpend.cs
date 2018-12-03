using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goyada.Actions
{
    public class QueryCardSpend : ActionBase, IAction
    {
        [JsonIgnore]
        public string Method => "querycardspend";

        [JsonProperty("cardId")]
        public string CardId { get; set; }

        [JsonProperty("cardPassword")]
        public string CardPassword { get; set; }

    }
}
