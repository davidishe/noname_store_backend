using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


/// <summary>
/// Состояние отправки чека по 54-ФЗ
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ReceiptRegistrationStatus
{
  [EnumMember(Value = "pending")]
  Pending = 1,
  [EnumMember(Value = "succeeded")]
  Succeeded,
  [EnumMember(Value = "canceled")]
  Canceled
}