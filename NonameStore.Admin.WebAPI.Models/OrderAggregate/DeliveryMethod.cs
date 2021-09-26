using System.ComponentModel.DataAnnotations.Schema;

namespace NonameStore.Admin.WebAPI.Models.Models
{
  public class DeliveryMethod : BaseEntity
  {

    public string ShortName { get; set; }
    public string DeliveryTime { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }

  }
}