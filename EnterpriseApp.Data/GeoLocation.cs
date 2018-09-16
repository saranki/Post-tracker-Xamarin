using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseApp.Data
{
    public class GeoLocation
    {
        [JsonProperty(PropertyName = "lat")]
        public double? Latitude { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public double? Longitude { get; set; }
    }
}
