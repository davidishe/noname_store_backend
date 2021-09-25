using MyAppBack.Models;

namespace MyAppBack.Dtos.Product
{
  public class ProductToCreate
  {
    public int? Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string? Description { get; set; }
    public int ProductTypeId { get; set; }
    public int ProductRegionId { get; set; }
    public int? Quantity { get; set; }
    public int GuId { get; set; }
  }
}