using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goyada.Actions
{
    public class Settle : ActionBase, IAction
    {
        [JsonIgnore]
        public string Method => "settle";
        
        [JsonProperty("orderReference")]
        public string OrderReference { get; set; }
        
        [JsonProperty("authorizationId")]
        public string AuthorizationId { get; set; }
    }
}
