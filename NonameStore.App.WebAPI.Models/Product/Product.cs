using System;

namespace NonameStore.App.WebAPI.Models
{
  public class Product : BaseEntity
  {
    public Product()
    {
    }

    public Product(string name, int price, string pictureUrl, string? description, int productTypeId, ProductType? type, int productRegionId, ProductRegion? region, int? quantity)
    {
      Name = name;
      Price = price;
      PictureUrl = pictureUrl;
      Description = description;
      ProductTypeId = productTypeId;
      Type = type;
      ProductRegionId = productRegionId;
      Region = region;
      Quantity = quantity;
      GuId = GetGuId();
      IsSale = false;
    }

    public string Name { get; set; }
    public int Price { get; set; }
    public string? PictureUrl { get; set; }
    public string? Description { get; set; }
    public ProductType Type { get; set; }
    public int ProductTypeId { get; set; }
    public ProductRegion Region { get; set; }
    public int ProductRegionId { get; set; }
    public int? Quantity { get; set; }
    public DateTime? EnrolledDate { get; set; } = DateTime.Now;
    public int GuId { get; set; }
    public bool IsSale { get; set; }


    private int GetGuId()
    {
      int i = Guid.NewGuid().GetHashCode();
      return i;
    }

  }



}
