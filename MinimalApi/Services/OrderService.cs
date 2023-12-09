using MinimalApi.Model;

namespace MinimalApi.Services

{
    public interface IOrderService
    {
        Order GetById(Guid id);
        List<Order> GetAll();
        void Add(Order order);
        void Remove(Guid id);
        void Update(Order order);
    }
    public class OrderService : IOrderService
    {
        public OrderService()
        {
            var sampleTask = new Order { Value = "Test Order" };
            _orders[sampleTask.Id] = sampleTask;
        }

        private readonly Dictionary<Guid, Order> _orders = new();//new Dictionary<Guid, Order>();

        public Order GetById(Guid id)
        {
            return _orders.GetValueOrDefault(id);
        }
        public List<Order> GetAll()
        {
            return _orders.Values.ToList();
        }
        public void Add(Order order)
        {
            if(order is null)
            {
                return;
            }
            _orders[order.Id] = order;
        }
        public void Remove(Guid id)
        {
            _orders.Remove(id);
        }
        public void Update(Order order)
        {
            var existingOrder = GetById(order.Id);
            if (existingOrder != null)
            {
                return;
            }
            _orders[order.Id] = order;
        }
    }


}
