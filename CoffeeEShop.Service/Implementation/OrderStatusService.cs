using CoffeeEShop.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeEShop.Service.Implementation
{
    public class OrderStatusService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<OrderStatusService> _logger;
        private readonly string[] _statuses = { "Pending", "Preparing", "On the way", "Delivered" };

        public OrderStatusService(IServiceScopeFactory scopeFactory, ILogger<OrderStatusService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OrderStatusService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

                    
                    var pendingOrders = orderService.GetAllNonDeliveredOrders(); 

                    foreach (var order in pendingOrders)
                    {
                        var idx = Array.IndexOf(_statuses, order.Status ?? "Pending");
                        if (idx < 0) idx = 0;
                        if (idx < _statuses.Length - 1)
                        {
                            var newStatus = _statuses[idx + 1];
                            orderService.UpdateStatus(order.Id, newStatus);
                            _logger.LogInformation("Order {OrderId} -> {Status}", order.Id, newStatus);
                        }
                    }
                }
                catch (OperationCanceledException) { /* shutting down */ }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in OrderStatusService loop.");
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

            _logger.LogInformation("OrderStatusService stopping.");
        }
    }
    }
