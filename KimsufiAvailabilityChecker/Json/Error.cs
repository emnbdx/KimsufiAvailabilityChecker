using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimsufiAvailabilityChecker.Json
{
    public class Error
    {
        [JsonProperty("__class")]
        public string Class { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("exceptionType")]
        public string ExceptionType { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
