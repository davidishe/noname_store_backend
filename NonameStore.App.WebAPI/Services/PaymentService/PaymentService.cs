using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NonameStore.App.WebAPI.Data.Repos.BasketRepository;
using NonameStore.App.WebAPI.Data.Spec;
using NonameStore.App.WebAPI.Data.UnitOfWork;
using NonameStore.App.WebAPI.Models;
using NonameStore.App.WebAPI.Models.OrderAggregate;
using NonameStore.App.WebAPI.Models.Payment;
using Order = NonameStore.App.WebAPI.Models.OrderAggregate.Order;
using Product = NonameStore.App.WebAPI.Models.Product;

namespace NonameStore.App.WebAPI.Services.PaymentService
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

        // 1.???????????????? ???????????? ?? ???????????????? ???????????? ?????? ????????????
        var newPayment = new NewPayment
        {
          Amount = new Amount
          {
            Value = (long)basket.Items.Sum(i => (i.Quantity * i.Price)) + (long)(shippingPrice),
            Currency = "RUB"
          },
          Capture = true,
          Description = "???????????? ???? ????????????",
          Confirmation = new Confirmation
          {
            Type = ConfirmationType.Redirect,
            ReturnUrl = _domainAddress + "/orders"
          }
        };


        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
          // PaymentIntentId = idempotenceKey
          // TODO: ???????????????? ?????? ?? idempotenceKey
          // ???????????????????? idempotenceKey ???????? PaymentIntentId ?? ?????????????? ????????????
          Payment payment = client.CreatePayment(newPayment);
          basket.PaymentIntentId = payment.Id;
          basket.ConfirmationUrl = payment.Confirmation.ConfirmationUrl;

        }
        else
        {
          // ???????? PaymentIntentId ?? ?????????????? ???? ????????????, ???????????????????? ?????? ?? ??????????????
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