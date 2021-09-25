using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NonameStore.App.WebAPI.Models.Payment
{
  /// <summary>
  /// Признак способа расчета
  /// </summary>
  [JsonConverter(typeof(StringEnumConverter))]
  public enum PaymentMode
  {
    /// <summary>
    /// Полная предоплата
    /// </summary>
    [EnumMember(Value = "full_prepayment")]
    FullPrepayment,

    /// <summary>
    /// Частичная предоплата
    /// </summary>
    [EnumMember(Value = "partial_prepayment")]
    PartialPrepayment,

    /// <summary>
    /// Аванс
    /// </summary>
    [EnumMember(Value = "advance")]
    Advance,

    /// <summary>
    /// Полный расчет
    /// </summary>
    [EnumMember(Value = "full_payment")]
    FullPayment,

    /// <summary>
    /// Частичный расчет и кредит
    /// </summary>
    [EnumMember(Value = "partial_payment")]
    PartialPayment,

    /// <summary>
    /// Кредит
    /// </summary>
    [EnumMember(Value = "credit")]
    Credit,

    /// <summary>
    /// Выплата по кредиту
    /// </summary>
    [EnumMember(Value = "credit_payment")]
    CreditPayment
  }
}