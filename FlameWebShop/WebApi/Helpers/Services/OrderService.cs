﻿using Microsoft.AspNetCore.Identity;
using WebApi.Models.Dtos;
using WebApi.Models.Email;
using WebApi.Models.Entities;
using WebApi.Models.Interfaces;
using WebApi.Models.Schemas;
using WebApi.Helpers.Repositories;

namespace WebApi.Helpers.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMailService _mailService;
        private readonly AddressRepository _addressRepo;
        private readonly IProductService _productService;
        private readonly ProductRepository _productRepo;

        public OrderService(OrderRepository orderRepo, UserManager<IdentityUser> userManager, IMailService mailService, AddressRepository addressRepo, IProductService productService, ProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _userManager = userManager;
            _mailService = mailService;
            _addressRepo = addressRepo;
            _productService = productService;
            _productRepo = productRepo;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            try
            {
                var currentDate = DateTime.Now;
                var orders = await _orderRepo.GetAllAsync();
                var dtos = new List<OrderDto>();

                foreach (var entity in orders)
                {
                    var status = entity.OrderStatus;
                    var orderDate = entity.OrderDate;

                    TimeSpan diff = currentDate - orderDate;
                    var daysDiff = diff.Days;

                    if (status != "Cancelled" && status != "Delivered")
                    {
                        entity.OrderStatus = daysDiff switch
                        {
                            > 1 and < 3 => "Shipped",
                            >= 3 => "Delivered",
                            _ => entity.OrderStatus
                        };

                        await _orderRepo.UpdateAsync(entity);
                    }

                    dtos.Add(entity);
                }

                return dtos;
            }
            catch { }
            return null!;
        }

        public async Task<OrderDto> GetByOrderIdAsync(int orderId)
        {
            try
            {
                var currentDate = DateTime.Now;
                var order = await _orderRepo.GetAsync(x => x.Id == orderId);

                var status = order.OrderStatus;
                var orderDate = order.OrderDate;

                TimeSpan diff = currentDate - orderDate;
                var daysDiff = diff.Days;

                if (status != "Cancelled" && status != "Delivered")
                {
                    order.OrderStatus = daysDiff switch
                    {
                        > 1 and < 3 => "Shipped",
                        >= 3 => "Delivered",
                        _ => order.OrderStatus
                    };

                    await _orderRepo.UpdateAsync(order);
                }

                OrderDto dto = order;

                return dto;
            }
            catch { }
            return null!;
        }

        public async Task<IEnumerable<OrderDto>> GetBySignedInUser(string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                var userId = int.Parse(user!.Id);

                var orders = await _orderRepo.GetListAsync(x => x.UserId == userId);
                var currentDate = DateTime.Now;
                var dtos = new List<OrderDto>();

                foreach (var item in orders)
                {
                    var status = item.OrderStatus;
                    var orderDate = item.OrderDate;

                    TimeSpan diff = currentDate - orderDate;
                    var daysDiff = diff.Days;

                    if (status != "Cancelled" && status != "Delivered")
                    {
                        item.OrderStatus = daysDiff switch
                        {
                            > 1 and < 3 => "Shipped",
                            >= 3 => "Delivered",
                            _ => item.OrderStatus
                        };

                        await _orderRepo.UpdateAsync(item);
                    }

                    dtos.Add(item);
                }

                return dtos;
            }
            catch { }
            return null!;
        }

        public async Task<IEnumerable<OrderDto>> GetByUserIdAsync(int userId)
        {
            try
            {
                var orders = await _orderRepo.GetListAsync(x => x.UserId == userId);
                var currentDate = DateTime.Now;
                var dtos = new List<OrderDto>();

                foreach (var item in orders)
                {
                    var status = item.OrderStatus;
                    var orderDate = item.OrderDate;

                    TimeSpan diff = currentDate - orderDate;
                    var daysDiff = diff.Days;

                    if (status != "Cancelled" && status != "Delivered")
                    {
                        item.OrderStatus = daysDiff switch
                        {
                            > 1 and < 3 => "Shipped",
                            >= 3 => "Delivered",
                            _ => item.OrderStatus
                        };

                        await _orderRepo.UpdateAsync(item);
                    }

                    dtos.Add(item);
                }

                return dtos;
            }
            catch { }
            return null!;
        }

        public async Task<bool> CancelOrder(OrderCancelSchema schema)
        {
            try
            {
                var order = await _orderRepo.GetAsync(x => x.Id == schema.OrderId);
                order.OrderStatus = "Cancelled";

                await _orderRepo.UpdateAsync(order);

                return true;
            }
            catch { }
            return false;
        }

        public async Task<bool> CreateOrderAsync(OrderSchema schema, string userEmail)
        {
            try
            {
                var orderItems = schema.Items;
                var user = await _userManager.FindByEmailAsync(userEmail);
                var address = await _addressRepo.GetAsync(x => x.Id == schema.AddressId);

                if (address != null)
                {
                    var order = new OrderEntity
                    {
                        OrderDate = DateTime.Now,
                        OrderStatus = "Pending",
                        Items = new List<OrderItemEntity>(),
                        UserId = int.Parse(user!.Id),
                        Address = address,
                        Price = schema.Price
                    };

                    foreach (var item in orderItems)
                    {
                        var product = await _productRepo.GetAsync(x => x.Id == item.ProductId);

                        var orderItem = new OrderItemEntity
                        {
                            ProductId = item.ProductId,
                            ProductName = product.Name,
                            UnitPrice = product.Price,
                            ImageUrl = product.ImageUrl,

                            Quantity = item.Quantity,
                        };
                        order.Items.Add(orderItem);
                    }

                    await _orderRepo.AddAsync(order);

                    var email = new MailData(new List<string> { userEmail }, "Order confirmation", $"Your order with Id: {order.Id} has been recieved! We will ship your items to you shortly.");
                    var result = await _mailService.SendAsync(email, new CancellationToken());

                    return true;
                }


            }
            catch { }
            return false;
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            try
            {
                var entity = await _orderRepo.GetAsync(x => x.Id == orderId);
                await _orderRepo.DeleteAsync(entity);

                return true;
            }
            catch { }
            return false;
        }
    }
}
