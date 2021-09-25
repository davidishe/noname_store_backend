using System.Runtime.Serialization;

namespace NonameStore.App.WebAPI.Models.OrderAggregate
{
  public enum PaymentMethod
  {
    [EnumMember(Value = "Оплата наличными")]
    PaymentCash,

    [EnumMember(Value = "Оплата онлайн")]
    PaymentOnline

  }
}