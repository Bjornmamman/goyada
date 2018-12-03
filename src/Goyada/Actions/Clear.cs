using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goyada.Actions
{
    public class Clear : ActionBase, IAction
    {
        [JsonIgnore]
        public string Method => "clear";
        
        [JsonProperty("orderReference")]
        public string OrderReference { get; set; }
        
        [JsonProperty("authorizationId")]
        public string AuthorizationId { get; set; }

        [JsonProperty("clearingAmount")]
        public int? ClearingAmount { get; set; } = null;

    }
}
