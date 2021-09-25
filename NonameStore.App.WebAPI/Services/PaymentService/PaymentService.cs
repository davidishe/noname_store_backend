using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyAppBack.Data.Repos.BasketRepository;
using MyAppBack.Data.Spec;
using MyAppBack.Data.UnitOfWork;
using MyAppBack.Models;
using MyAppBack.Models.OrderAggregate;
using MyAppBack.Models.Payment;
using Order = MyAppBack.Models.OrderAggregate.Order;
using Product = MyAppBack.Models.Product;

namespace MyAppBack.Services.PaymentService
{
  public class PaymentService : IPaymentService
  {
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _config;
    private readonly string _domainAddress;
    private readonly string _shopId;
    private readonly string _secretKey;



    public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration config)
    {
      _config = config;
      _unitOfWork = unitOfWork;
      _basketRepository = basketRepository;
      _domainAddress = config.GetSection("UserSettings:DomainAddress").Value;
      _shopId = config.GetSection("UserSettings:ShopId").Value;
      _secretKey = config.GetSection("UserSettings:SecretKey").Value;
    }

    public async Task<Basket> CreateOrUpdatePaymentIntent(string basketId)
    {

      var basket = await _basketRepository.GetBasketAsync(basketId);
      var shippingPrice = 0;

      if (basket == null) return null;

      if (basket.DeliveryMethodId.HasValue)
      {

        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync((int)basket.DeliveryMethodId);
        shippingPrice = deliveryMethod.Price;

        foreach (var item in basket.Items)
        {
          var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
          if (item.Price != productItem.Price)
          {
            item.Price = productItem.Price;
          }
        }

        var client = new Client(_shopId, _secretKey);

        // 1.Создайте платеж и получите ссылку для оплаты
        var newPayment = new NewPayment
        {
          Amount = new Amount
          {
            Value = (long)basket.Items.Sum(i => (i.Quantity * i.Price)) + (long)(shippingPrice),
            Currency = "RUB"
          },
          Capture = true,
          Description = "Оплата по заказу",
          Confirmation = new Confirmation
          {
            Type = ConfirmationType.Redirect,
            ReturnUrl = _domainAddress + "orders"
          }
        };


        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
          // PaymentIntentId = idempotenceKey
          // TODO: привести все к idempotenceKey
          // генерируем idempotenceKey если PaymentIntentId у корзины пустой
          Payment payment = client.CreatePayment(newPayment);
          basket.PaymentIntentId = payment.Id;
          basket.ConfirmationUrl = payment.Confirmation.ConfirmationUrl;

        }
        else
        {
          // если PaymentIntentId у корзины не пустой, используем его в запросе
          newPayment.Confirmation.ReturnUrl = newPayment.Confirmation.ReturnUrl + basket.PaymentIntentId;
          Payment payment = client.CreatePayment(newPayment, basket.PaymentIntentId);
          basket.PaymentIntentId = payment.Id;
          basket.ConfirmationUrl = payment.Confirmation.ConfirmationUrl;
        }

        await _basketRepository.UpdateBasketAsync(basket);
        return basket;
      }

      return null;
    }

    public async Task<Order> UpdateOrderPaymentFailed(string paymentInentId)
    {
      var spec = new OrderByPaymentIntentIdSpecification(paymentInentId);
      var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
      if (order == null) return null;
      order.Status = OrderStatus.PaymentFailed;
      _unitOfWork.Repository<Order>().Update(order);
      await _unitOfWork.Complete();
      return order;
    }

    public async Task<Order> UpdateOrderPaymentSucceded(string paymentInentId)
    {
      var spec = new OrderByPaymentIntentIdSpecification(paymentInentId);
      var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
      if (order == null) return null;
      order.Status = OrderStatus.PaymentReceived;
      _unitOfWork.Repository<Order>().Update(order);
      await _unitOfWork.Complete();
      return order;
    }



    public async Task<Basket> CreateOrUpdateOffline(string basketId)
    {

      var basket = await _basketRepository.GetBasketAsync(basketId);
      var shippingPrice = 0;

      if (basket == null) return null;

      if (basket.DeliveryMethodId.HasValue)
      {

        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync((int)basket.DeliveryMethodId);
        shippingPrice = deliveryMethod.Price;

        foreach (var item in basket.Items)
        {
          var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
          if (item.Price != productItem.Price)
          {
            item.Price = productItem.Price;
          }
        }

        await _basketRepository.UpdateBasketAsync(basket);
        return basket;
      }

      return null;
    }
  }
}