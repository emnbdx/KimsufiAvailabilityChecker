using Newtonsoft.Json;

namespace KimsufiAvailabilityChecker.Json
{
    public class Availability
    {
        [JsonProperty("__class")]
        public string Class { get; set; }

        [JsonProperty("displapyMetazones")]
        public int DisplayMetazones { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("metaZones")]
        public Zone[] MetaZones { get; set; }

        [JsonProperty("zones")]
        public Zone[] Zones { get; set; }
    }
}