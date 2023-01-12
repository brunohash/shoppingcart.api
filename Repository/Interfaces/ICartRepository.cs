using System;
using ShoppingCart.Models;

namespace ShoppingCart.Repository.Interfaces
{
    public interface ICartRepository
    {
        Task<CartModel> GenerateToken();

        Task<int> AddItem(CartItem items);

        Task<IEnumerable<CartItem>> GetAllItemsCart(string token);

        Task<CartComplete> GetCartInfo(string token);

        Task<bool> CheckCartExists(string token);

        Task<bool> CheckAuthorizationCart(string token);

        Task<int> DeleteCart(string token);

        Task<IEnumerable<CartModel>> GetAllCartToken();

        Task<int> UpdateStatusCart(string token, int status);

        Task<int> DeleteItemCart(string token, int itemId);
    }
}

