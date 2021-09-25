using System.Runtime.Serialization;

namespace NonameStore.App.WebAPI.Models.OrderAggregate
{
  public enum OrderStatus
  {
    [EnumMember(Value = "Заказ ожидает оплаты")]
    Pending,

    [EnumMember(Value = "Оплата получена, готовим заказ к отправке")]
    PaymentReceived,

    [EnumMember(Value = "Платеж не прошел, мы свяжемся с вами")]
    PaymentFailed,

    [EnumMember(Value = "Заказ передан в доставку")]
    OrderShiped,

    [EnumMember(Value = "Заказ доставлен")]
    OrderDelivered
  }
}