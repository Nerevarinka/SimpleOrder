namespace SimpleOrder.Services
{
    using Microsoft.EntityFrameworkCore;
    using SimpleOrder.Models;
    using System.Diagnostics.Metrics;

    public class OrderService
    {
        public SimpleOrderContext Context { get; }

        public OrderService(SimpleOrderContext context)
        {
            Context = context;
        }

        public async Task<OrderDisplayViewModel> Read(Guid orderId)
        {
            if (orderId == default)
            {
                throw new ArgumentNullException(nameof(orderId), "No value");
            }

            Order order = await Context
                .Set<Order>()
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == default)
            {
                throw new Exception($"Order with id \"{orderId}\" not found");
            }

            return MapToDisplayViewModel(order);
        }

        public async Task<IEnumerable<OrderDisplayViewModel>> ReadAll()
        {
            return await Context.Set<Order>()
                .Select(x => MapToDisplayViewModel(x))
                .ToListAsync();
        }

        public async Task Create(OrderCreateViewModel orderData)
        {
            if (orderData == default)
            {
                throw new ArgumentNullException(nameof(orderData), "No value");
            }

            ValidateEntity(orderData);

            orderData.ShipmentDate = DateTime.SpecifyKind(orderData.ShipmentDate, DateTimeKind.Utc);

            Order newOrder = MapToEntity(orderData);

            await Context.Set<Order>().AddAsync(newOrder);
            await Context.SaveChangesAsync();
        }

        public async Task Update(Guid orderId, OrderEditViewModel orderNewData)
        {
            if (orderId == default)
            {
                throw new ArgumentNullException(nameof(orderId), "No value");
            }

            if (orderNewData == default)
            {
                throw new ArgumentNullException(nameof(orderNewData), "No value");
            }

            Order order = await Context
                .Set<Order>()
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == default)
            {
                throw new Exception($"Order with id \"{orderId}\" not found");
            }

            ValidateEntity(orderNewData);

            await Context.SaveChangesAsync();
        }

        public async Task Delete(Guid orderId)
        {
            if (orderId == default)
            {
                throw new ArgumentNullException("Id has no value");
            }

            Order order = await Context
                .Set<Order>()
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == default)
            {
                throw new Exception($"Order with id \"{orderId}\" not found");
            }

            Context.Set<Order>().Remove(order);
            await Context.SaveChangesAsync();
        }

        private static OrderDisplayViewModel MapToDisplayViewModel(Order order)
        {
            return new OrderDisplayViewModel()
            {
                Id = order.Id,
                Number = order.Number,
                RecipientAddress = order.RecipientAddress,
                RecipientCity = order.RecipientCity,
                SenderAddress = order.SenderAddress,
                SenderCity = order.SenderCity,
                Weight = order.Weight,
                ShipmentDate = order.ShipmentDate
            };
        }

        private Order MapToEntity(OrderCreateViewModel orderData)
        {
            return new Order()
            {
                Id = Guid.NewGuid(),
                RecipientAddress = orderData.RecipientAddress,
                RecipientCity = orderData.RecipientCity,
                SenderAddress = orderData.SenderAddress,
                SenderCity = orderData.SenderCity,
                Weight = orderData.Weight,
                ShipmentDate = orderData.ShipmentDate
            };
        }

        private void ValidateEntity(OrderViewModel orderData)
        {
            if (string.IsNullOrEmpty(orderData.SenderCity) ||
                string.IsNullOrEmpty(orderData.SenderAddress) ||
                string.IsNullOrEmpty(orderData.RecipientCity) ||
                string.IsNullOrEmpty(orderData.RecipientAddress)
                )
            {
                throw new ArgumentException("No value");
            }

            if (orderData.Weight < 0.001 || orderData.Weight > 100_000)
            {
                throw new ArgumentException(nameof(orderData.Weight), "Incorrect value");
            }
        }
    }
}
