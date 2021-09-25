using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyAppBack.Models.Payment
{
  [JsonConverter(typeof(StringEnumConverter))]
  public enum VatDataType
  {
    [EnumMember(Value = "calculated")] Calculated,
    [EnumMember(Value = "untaxed")] Untaxed
  }
}