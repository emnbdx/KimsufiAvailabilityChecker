using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KimsufiAvailabilityChecker.Json
{
    public class Answer
    {
        [JsonProperty("__class")]
        public string Class { get; set; }

        [JsonProperty("availability")]
        public Availability[] Availabilities { get; set; }
    }
}
