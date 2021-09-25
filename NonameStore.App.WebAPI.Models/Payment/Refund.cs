using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NonameStore.App.WebAPI.Models.Payment
{
  /// <inheritdoc />
  /// <summary>
  /// Информация о возврате
  /// </summary>
  public class Refund : NewRefund
  {
    /// <summary>
    /// Идентификатор возврата
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public PaymentStatus Status { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }

    public ReceiptRegistrationStatus? ReceiptRegistration { get; set; }
  }
}