using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyAppBack.Data.Repos.BasketRepository;
using MyAppBack.Data.Spec;
using MyAppBack.Data.UnitOfWork;
using MyAppBack.Models;
using MyAppBack.Models.OrderAggregate;
using MyAppBack.Services.PaymentService;

namespace MyAppBack.Services.OrderService
{
  public class OrderService : IOrderService
  {
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentService _paymentService;
    public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo, IPaymentService paymentService)
    {
      _paymentService = paymentService;
      _unitOfWork = unitOfWork;
      _basketRepo = basketRepo;
    }

    public async Task<Order> CreateOrderAsync(string byerEmail, int deliveryMethodId, string basketId, Address shipingAddress, PaymentMethod paymentMethod)
    {
      // get basket from repo
      var basket = await _basketRepo.GetBasketAsync(basketId);

      // get items from the product repo
      var items = new List<OrderItem>();
      foreach (var item in basket.Items)
      {
        var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
        var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl, productItem.GuId);
        var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity, item.GuId);
        items.Add(orderItem);
      }

      // get delivery method from repo
      var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

      // calc subtotal
      var subtotal = items.Sum(item => (item.Price * item.Quantity));

      // check paymentIntentId already exists
      var spec = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
      var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec); // with id!
      if (existingOrder != null)
      {
        _unitOfWork.Repository<Order>().Delete(existingOrder);

        if (paymentMethod == PaymentMethod.PaymentOnline)
        {
          await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
        }
        if (paymentMethod == PaymentMethod.PaymentCash)
        {
          await _paymentService.CreateOrUpdateOffline(basket.Id);
        }
      }

      // create order
      var order = new Order(byerEmail, shipingAddress, deliveryMethod, items, subtotal, basket.PaymentIntentId, basket.OrderNumber, paymentMethod);
      _unitOfWork.Repository<Order>().Add(order);

      // TO DO: save to db
      var result = await _unitOfWork.Complete();
      if (result <= 0) return null;

      // delete basket
      await _basketRepo.DeleteBasketAsync(basketId);

      // return order
      return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
      return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
    }

    public async Task<Order> GetOrderById(int id, string byerEmail)
    {
      var spec = new OrderWithItemsAndOrderingSpecification(id, byerEmail);
      return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string byerEmail)
    {
      var spec = new OrderWithItemsAndOrderingSpecification(byerEmail);
      var orders = await _unitOfWork.Repository<Order>().ListAsync(spec);
      return orders;
    }

    public Task<Order> GetOrderByPaymentIntent(string paymentIntentId)
    {
      var order = _unitOfWork.Repository<Order>().GetOrderByPaymentIntent(paymentIntentId);
      return order;
    }








    public async Task<Order> UpdateOrderAsync(int orderId, int deliveryMethodId)
    {
      // get basket from repo
      var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);

      // get items from the product repo
      var items = new List<OrderItem>();

      // get delivery method from repo
      var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

      // calc subtotal again
      var subtotal = items.Sum(item => (item.Price * item.Quantity));

      // create order
      var updatedOrder = new Order(order.ByerEmail, order.ShipToAddress, deliveryMethod, order.OrderItems, subtotal, order.PaymentIntentId, order.OrderNumber, order.PaymentMethod);
      _unitOfWork.Repository<Order>().Add(order);

      // TO DO: save to db
      var result = await _unitOfWork.Complete();
      if (result <= 0) return null;

      // return order
      return updatedOrder;
    }





  }
}