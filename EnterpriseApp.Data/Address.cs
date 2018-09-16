using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseApp.Data
{
    public class Address
    {
        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "suite")]
        public string Suite { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "zipcode")]
        public string ZipCode { get; set; }

        [JsonProperty(PropertyName = "geo")]
        public GeoLocation Geo { get; set; }
    }
}
