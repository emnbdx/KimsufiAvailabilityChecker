using Newtonsoft.Json;

namespace KimsufiAvailabilityChecker.Json
{
    public class Root
    {
        [JsonProperty("answer")]
        public Answer Answer { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}