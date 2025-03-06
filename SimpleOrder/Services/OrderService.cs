namespace SimpleOrder.Services
{
    using Microsoft.EntityFrameworkCore;
    using SimpleOrder.Exceptions;
    using SimpleOrder.Models;
    using SimpleOrder.Models.NewFolder;

    public class OrderService
    {
        private const decimal MIN_WEIGHT = 0.001M;

        private const decimal MAX_WEIGHT = 100_000_000M;

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
                throw new EntityNotFoundException($"Order with id \"{orderId}\" not found");
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

            // Explicitly set date format to UTC for correct validation
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
                throw new EntityNotFoundException($"Order with id \"{orderId}\" not found");
            }

            ValidateEntity(orderNewData);
            UpdateFieldValues(order, orderNewData);

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
                return;
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
                CreatedAt = order.CreatedAt,
                ShipmentDate = order.ShipmentDate
            };
        }

        private static Order MapToEntity(OrderCreateViewModel orderData)
        {
            return new Order()
            {
                Id = Guid.NewGuid(),
                RecipientAddress = orderData.RecipientAddress,
                RecipientCity = orderData.RecipientCity,
                SenderAddress = orderData.SenderAddress,
                SenderCity = orderData.SenderCity,
                Weight = orderData.Weight,
                CreatedAt = DateTime.UtcNow,
                ShipmentDate = orderData.ShipmentDate
            };
        }

        private static void ValidateEntity(OrderViewModel orderData)
        {
            if (string.IsNullOrEmpty(orderData.SenderCity) ||
                string.IsNullOrEmpty(orderData.SenderAddress) ||
                string.IsNullOrEmpty(orderData.RecipientCity) ||
                string.IsNullOrEmpty(orderData.RecipientAddress)
                )
            {
                throw new ArgumentException("No value");
            }

            if (orderData.Weight is < MIN_WEIGHT or > MAX_WEIGHT)
            {
                throw new ArgumentException(nameof(orderData.Weight), "Incorrect value");
            }
        }

        private static void UpdateFieldValues(Order order, OrderEditViewModel orderNewData)
        {
            order.RecipientAddress = orderNewData.RecipientAddress;
            order.RecipientCity = orderNewData.RecipientCity;
            order.SenderAddress = orderNewData.SenderAddress;
            order.SenderCity = orderNewData.SenderCity;
            order.Weight = orderNewData.Weight;
            order.ShipmentDate = orderNewData.ShipmentDate;
        }
    }
}
