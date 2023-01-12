using ShoppingCart.Models;
using Dapper;
using MySqlConnector;
using ShoppingCart.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Collections.Generic;
using System.Collections;
using System.Net.NetworkInformation;

namespace ShoppingCart.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IDbConnection _mySql;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _entityId;

        public CartRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _mySql = new MySqlConnection(config.GetConnectionString("mySqlGeneral"));
            _httpContextAccessor = httpContextAccessor;
            _entityId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<CartModel> GenerateToken()
        {
            try
            {

                CartModel cartCreated = new CartModel();
                cartCreated.TokenCart = Guid.NewGuid().ToString("N");
                cartCreated.EntityId = _entityId;
                cartCreated.Status = 1;
                cartCreated.Created_at = DateTime.Today;

                int result = await _mySql.ExecuteAsync(@"
                INSERT INTO `cartShopping`.`CartConfiguration`
                (`TokenCart`, `EntityId`, `Status`, `Created_at`)
                VALUES (@TokenCart, @Entityid, @Status, @Created_at);",
                new
                {
                    cartCreated.TokenCart,
                    cartCreated.EntityId,
                    cartCreated.Status,
                    cartCreated.Created_at
                });

                return cartCreated;
         
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<int> AddItem(CartItem items)
        {
            try
            {
                int result = await _mySql.ExecuteAsync(@"
                INSERT INTO `cartShopping`.`CartItem`
                (`TokenCart`, `ExternalId`, `Name`, `Price`, `Amount`, `Variable`)
                VALUES (@TokenCart, @ExternalId, @Name, @Price, @Amount, @Variable);",
                new
                {
                    TokenCart = items.TokenCart,
                    ExternalId = items.ExternalId,
                    Name = items.Name,
                    Price = items.Price,
                    Amount = items.Amount,
                    Variable = items.Variable
                });

                return result; 
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<CartComplete> GetCartInfo(string tokenId)
        {
            CartModel result =  await _mySql.QueryFirstOrDefaultAsync<CartModel>(@"
                        SELECT * FROM `cartShopping`.`CartConfiguration`
                        WHERE `EntityId` = @EntityId AND `TokenCart` = @TokenCart
                      ",
                    new
                    {
                        EntityId = _entityId,
                        TokenCart = tokenId
                    });

            CartComplete cart = new CartComplete();
            cart.EntityId = _entityId;
            cart.TokenCart = result.TokenCart;
            cart.Status = result.Status;
            cart.Created_at = result.Created_at;
            cart.CartItem = await GetAllItemsCart(result.TokenCart);

            return cart;
        }

        public async Task<IEnumerable<CartModel>> GetAllCartToken()
        {
            return await _mySql.QueryAsync<CartModel>(@"
                        SELECT * FROM `cartShopping`.`CartConfiguration`
                        WHERE `EntityId` = @EntityId AND `Status` >= 1
                      ",
                    new
                    {
                        EntityId = _entityId
                    });
        }

        public async Task<IEnumerable<CartItem>> GetAllItemsCart(string tokenId)
        {
            return await _mySql.QueryAsync<CartItem>(@"
                        SELECT * FROM `cartShopping`.`CartItem`
                        WHERE `TokenCart` = @TokenCart
                      ",
                    new
                    {
                        TokenCart = tokenId
                    });
        }

        public async Task<bool> CheckCartExists(string token)
        {
            try
            {
                return await _mySql.QueryFirstOrDefaultAsync<bool>(@"
                        SELECT * FROM `cartShopping`.`CartConfiguration`
                        WHERE `TokenCart` = @TokenCart
                      ",
                    new
                    {
                        TokenCart = token
                    });
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CheckAuthorizationCart(string token)
        {
            try
            {
                return await _mySql.QueryFirstOrDefaultAsync<bool>(@"
                        SELECT * FROM `cartShopping`.`CartConfiguration`
                        WHERE `TokenCart` = @TokenCart AND `EntityId` = @EntityId
                      ",
                    new
                    {
                        TokenCart = token,
                        EntityId = _entityId,
                    });
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> DeleteCart(string token)
        {
            return await _mySql.ExecuteAsync(@"
                DELETE FROM `cartShopping`.`CartConfiguration`
                WHERE `TokenCart` = @TokenCart AND `EntityId` = @EntityId",
                new
                {
                    TokenCart = token,
                    EntityId = _entityId
                });
        }

        public async Task<int> DeleteItemCart(string token, int itemId)
        {
            return await _mySql.ExecuteAsync(@"
                DELETE CartItem
                FROM CartItem
                INNER JOIN cartShopping.CartConfiguration ON cartShopping.CartConfiguration.TokenCart = cartShopping.CartItem.TokenCart
                WHERE CartConfiguration.TokenCart = @TokenCart AND CartConfiguration.EntityId = @EntityId AND CartItem.Id = @itemId;",
                new
                    {
                        TokenCart = token,
                        EntityId = _entityId,
                        ItemId = itemId
                    });
        }

        public async Task<int> UpdateStatusCart(string token, int status)
        {
            return await _mySql.ExecuteAsync(@"
                UPDATE `cartShopping`.`CartConfiguration`
                SET `Status` = @Status
                WHERE `EntityId` = @EntityId AND `TokenCart` = @TokenCart",
                new
                {
                    Status = status,
                    EntityId = _entityId,
                    TokenCart = token
                });
        }
    }
}

