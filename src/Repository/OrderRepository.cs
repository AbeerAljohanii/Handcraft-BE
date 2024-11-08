using Backend_Teamwork.src.Database;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Utils;
using Microsoft.EntityFrameworkCore;

namespace Backend_Teamwork.src.Repository
{
    public class OrderRepository
    {
        private readonly DbSet<Order> _order;
        private readonly DatabaseContext _databaseContext;

        public OrderRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _order = databaseContext.Set<Order>();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _order
                .Include(o => o.User) // Include User details for Order
                .Include(o => o.OrderDetails) // Include OrderDetails
                .ThenInclude(od => od.Artwork) // Include Artwork for OrderDetails
                .ThenInclude(a => a.Category) // Include Category within Artwork
                .Include(o => o.OrderDetails) // Re-include OrderDetails to chain Artwork's User
                .ThenInclude(od => od.Artwork) // Target Artwork in OrderDetails
                .ThenInclude(a => a.User) // Include User within Artwork
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _databaseContext
                .Order.Include(o => o.User) // Include User
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Artwork) // Include Artwork
                .ThenInclude(a => a.Category) // Include Category in Artwork
                .Where(order => order.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order?> CreateOneAsync(Order newOrder)
        {
            await _order.AddAsync(newOrder);
            await _databaseContext.SaveChangesAsync();
            return await GetByIdAsync(newOrder.Id);
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _order
                .Include(o => o.User) // Include User
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Artwork) // Include Artwork
                .ThenInclude(a => a.Category) // Include Category in Artwork
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> DeleteOneAsync(Order Order)
        {
            if (Order == null)
                return false;
            _order.Remove(Order);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOneAsync(Order updateOrder)
        {
            if (updateOrder == null)
                return false;
            _order.Update(updateOrder);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetAllAsync(PaginationOptions paginationOptions)
        {
            // Query for orders with optional search
            var orderQuery = _order
                .Include(o => o.OrderDetails) // Include order details
                .Include(o => o.User)
                .Where(o =>
                    o.ShippingAddress.Contains(paginationOptions.Search)
                    || o.TotalAmount.ToString().Contains(paginationOptions.Search)
                );

            // Apply pagination
            orderQuery = orderQuery
                .Skip((paginationOptions.PageNumber - 1) * paginationOptions.PageSize)
                .Take(paginationOptions.PageSize);

            // Sorting logic
            orderQuery = paginationOptions.SortOrder switch
            {
                "amount_desc" => orderQuery.OrderByDescending(o => o.TotalAmount),
                "amount_asc" => orderQuery.OrderBy(o => o.TotalAmount),
                "date_desc" => orderQuery.OrderByDescending(o => o.CreatedAt),
                "date_asc" => orderQuery.OrderBy(o => o.CreatedAt),
                _ => orderQuery.OrderBy(o => o.ShippingAddress), // Default sorting by ShippingAddress
            };

            return await orderQuery.ToListAsync();
        }
    }
}
