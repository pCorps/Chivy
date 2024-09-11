using ChivyPoo.Data.Models;
using ChivyPoo.Data;
using ChivyPoo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ChivyPoo.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> ClearCartAsync(string? userId)
        {
            var cartItems = await _db.ShoppingCart.Where(u => u.UserId == userId).ToListAsync();
            _db.ShoppingCart.RemoveRange(cartItems);
            return await _db.SaveChangesAsync() > 0; // > 0 means we did delete some items 
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
        {
            return await _db.ShoppingCart.Where(u => u.UserId == userId).Include(u => u.Product).ToListAsync();
        }

        public async Task<int> GetTotalCartCartCountAsync(string? userId)
        {
            int cartCount = 0;
            var cartItems = await _db.ShoppingCart.Where(u => u.UserId == userId).ToListAsync();

            foreach (var item in cartItems)
            {
                cartCount += item.Count;
            }
            return cartCount;
        }

        public async Task<bool> UpdateCartAsync(string userId, int productId, int updateBy)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            //identify if user id has product id in ccart
            var cart = await _db.ShoppingCart.FirstOrDefaultAsync(u => u.UserId == userId && u.ProductId == productId);
            if (cart == null) //we do not have a record in cart
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = productId,
                    Count = updateBy
                };

                await _db.ShoppingCart.AddAsync(cart);
            } else //we do have a record in cart
            {
                cart.Count += updateBy;
                if (cart.Count <= 0)
                {
                    _db.ShoppingCart.Remove(cart);
                }
            }
            return await _db.SaveChangesAsync() > 0;
        }
    }
}