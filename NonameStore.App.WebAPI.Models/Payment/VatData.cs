namespace NonameStore.App.WebAPI.Models.Payment
{
  public class VatData
  {
    public VatDataType Type { get; set; }
    public string Rate { get; set; }
    public Amount Amount { get; set; }
    public PayerBankDetails PayerBankDetails { get; set; }
  }
}