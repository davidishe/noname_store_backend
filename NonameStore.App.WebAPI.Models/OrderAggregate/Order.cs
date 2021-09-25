using System;
using System.Collections.Generic;

namespace NonameStore.App.WebAPI.Models.OrderAggregate
{
  public class Order : BaseEntity
  {
    public Order()
    {
    }

    public Order(string byerEmail, Address shipToAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems, int subtotal, string paymentIntentId, string orderNumber, PaymentMethod paymentMethod)
    {
      ByerEmail = byerEmail;
      ShipToAddress = shipToAddress;
      DeliveryMethod = deliveryMethod;
      OrderItems = orderItems;
      Subtotal = subtotal;
      PaymentIntentId = paymentIntentId;
      OrderNumber = orderNumber;
      PaymentMethod = paymentMethod;
    }

    public string ByerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public Address? ShipToAddress { get; set; }
    public DeliveryMethod DeliveryMethod { get; set; }
    public IReadOnlyList<OrderItem> OrderItems { get; set; }
    public int Subtotal { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string? PaymentIntentId { get; set; } = "0";
    public string? OrderNumber { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

  }
}