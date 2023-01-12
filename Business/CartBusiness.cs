using System;
using System.Collections.Generic;
using ShoppingCart.Models;
using ShoppingCart.Repository.Interfaces;
using ShoppingCart.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace ShoppingCart.Business
{
    public class CartBusiness : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartBusiness(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartModel> CreateCart()
        {
            CartModel token = await _cartRepository.GenerateToken();

            return token;
        }

        public async Task<string> AddItemtoCart(CartItem items)
        {
            bool cartExists = await _cartRepository.CheckCartExists(items.TokenCart);

            if(cartExists)
            {
                bool authorizationCart = await _cartRepository.CheckAuthorizationCart(items.TokenCart);

                if (authorizationCart)
                {
                    int token = await _cartRepository.AddItem(items);

                    if (token > 0)
                    {
                        return "Produto adicionado ao carrinho: " + items.TokenCart;
                    }
                    else
                    {
                        return "Ocorreu um erro ao adicionar o produto no carrinho.";
                    }
                }
                else
                {
                    return "Você não tem permissão para acessar esse carrinho.";
                }

            }
            else
            {
                return "Para adicionar um produto ao carrinho é necessário criar o carrinho antes.";
            }
             
        }

        public async Task<CartComplete> GetCompleteCart(string items)
        {
            CartComplete token = await _cartRepository.GetCartInfo(items);

            return token;
        }

        public async Task<IEnumerable<CartModel>> GetAllActiveCart()
        {
            IEnumerable<CartModel> result = await _cartRepository.GetAllCartToken();

            return result;
        }

        public async Task<bool> DeleteCart(string token)
        {
            int result = await _cartRepository.UpdateStatusCart(token, 0);

            if(result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteItemCart(string token, int itemId)
        {
            int result = await _cartRepository.DeleteItemCart(token, itemId);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateStatusCart(string token, int status)
        {
            int result = await _cartRepository.UpdateStatusCart(token, status);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

