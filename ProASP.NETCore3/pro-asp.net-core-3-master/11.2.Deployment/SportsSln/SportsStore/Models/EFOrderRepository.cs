using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private StoreDbContext context;
        public EFOrderRepository(StoreDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);    //Eager loading
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));    //This ensures that Entity Framework Core won’t try to write the de-serialized Product objects that are associated with the Order object. P206
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
