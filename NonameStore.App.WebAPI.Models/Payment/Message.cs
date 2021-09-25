using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NonameStore.App.WebAPI.Models.Payment
{
  public class Message
  {
    public string Type { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Event Event { get; set; }

    public Payment Object { get; set; }
  }
}