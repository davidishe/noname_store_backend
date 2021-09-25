using System;
using System.Collections.Generic;
using NonameStore.App.WebAPI.Models.OrderAggregate;

namespace NonameStore.App.WebAPI.Dtos
{
  public class OrderToReturnDto
  {
    public int Id { get; set; }
    public string ByerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public Address? ShipToAddress { get; set; }
    public string DeliveryMethod { get; set; }
    public int DeliveryPrice { get; set; }
    public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
    public int Subtotal { get; set; }
    public string Status { get; set; }
    public string PaymentIntentId { get; set; }
    public string? OrderNumber { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

  }
}