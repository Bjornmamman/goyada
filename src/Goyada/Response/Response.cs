using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Goyada.Response
{
    public class ResponseData
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("localizedMessage")]
        public string LocalizedMessage { get; set; }

        [JsonProperty("authorizationId")]
        public string AuthorizationId { get; set; }

        [JsonProperty("transaction")]
        public Transaction Transaction { get; set; }

        [JsonProperty("card")]
        public Card Card { get; set; }
    }
}
