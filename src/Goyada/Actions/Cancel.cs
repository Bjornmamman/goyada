using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goyada.Actions
{
    public class Cancel : ActionBase, IAction
    {
        [JsonIgnore]
        public string Method => "cancel";
        
        [JsonProperty("orderReference")]
        public string OrderReference { get; set; }
        
        [JsonProperty("authorizationId")]
        public string AuthorizationId { get; set; }

        [JsonProperty("cancelReason")]
        public string CancelReason { get; set; } = "no-reason";

    }
}
