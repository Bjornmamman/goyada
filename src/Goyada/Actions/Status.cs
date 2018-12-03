using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goyada.Actions
{
    public class Status : ActionBase, IAction
    {
        [JsonIgnore]
        public string Method => "status";
        
        [JsonProperty("orderReference")]
        public string OrderReference { get; set; }
        
        [JsonProperty("authorizationId")]
        public string AuthorizationId { get; set; }

    }
}
