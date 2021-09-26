using System.Runtime.Serialization;

namespace NonameStore.Admin.WebAPI.Models.Models
{
  public enum PaymentMethod
  {
    [EnumMember(Value = "Оплата наличными")]
    PaymentCash,

    [EnumMember(Value = "Оплата онлайн")]
    PaymentOnline

  }
}