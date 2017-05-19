using Newtonsoft.Json;

namespace KimsufiAvailabilityChecker.Json
{
    public class Zone
    {
        [JsonProperty("__class")]
        public string Class { get; set; }

        [JsonProperty("availability")]
        public string Availability { get; set; }

        [JsonProperty("zone")]
        public string Region { get; set; }
    }
}