using System.Runtime.Serialization;

namespace MyAppBack.Models.OrderAggregate
{
  public enum PaymentMethod
  {
    [EnumMember(Value = "Оплата наличными")]
    PaymentCash,

    [EnumMember(Value = "Оплата онлайн")]
    PaymentOnline

  }
}