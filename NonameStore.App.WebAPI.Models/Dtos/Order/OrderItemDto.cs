namespace NonameStore.App.WebAPI.Dtos
{
  public class OrderItemDto
  {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public int GuId { get; set; }

  }
}