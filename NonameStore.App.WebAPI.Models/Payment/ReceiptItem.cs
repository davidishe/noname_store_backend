using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NonameStore.App.WebAPI.Models.Payment
{
  /// <summary>
  /// Позиция чека
  /// <seealso cref="Receipt"/>
  /// </summary>
  public class ReceiptItem
  {
    /// <summary>
    /// Наименование товара
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// Стоимость
    /// </summary>
    public Amount Amount { get; set; }

    /// <summary>
    /// Код налога, <see cref="V3.VatCode"/>
    /// </summary>
    public VatCode VatCode { get; set; }

    /// <summary>
    /// Признак предмета расчета, <see cref="V3.PaymentSubject"/>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public PaymentSubject? PaymentSubject { get; set; }

    /// <summary>
    /// Признак способа расчета <see cref="V3.PaymentMode"/>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public PaymentMode? PaymentMode { get; set; }
  }
}