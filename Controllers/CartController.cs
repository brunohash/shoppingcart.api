using ShoppingCart.Business;
using ShoppingCart.Models;
using ShoppingCart.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Controllers;

[ApiController]
public class CartController : ControllerBase
{
    private readonly CartBusiness _cartBusiness;

    public CartController(CartBusiness cartBusiness)
    {
        _cartBusiness = cartBusiness;
    }

    [Authorize(Roles = "admin")]
    [HttpPost("v1/createcart")]
    public async Task<IActionResult> CreateCart()
    {
        try
        {
            CartModel result = await _cartBusiness.CreateCart();

            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao criar o carrinho.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Opss.. aconteceu algum problema, tente novamente mais tarde!");
        }
    }

    [Authorize(Roles = "admin")]
    [HttpPost("v1/addcart")]
    public async Task<IActionResult> AddItemtoCart([FromBody] CartItem items)
    {
        try
        {
            string result = await _cartBusiness.AddItemtoCart(items);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Erro ao adicionar produto ao carrinho.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Opss.. aconteceu algum problema, tente novamente mais tarde!");
        }
    }

    [Authorize(Roles = "admin")]
    [HttpGet("v1/{tokenId}/get")]
    public async Task<IActionResult> GetCompleteCart(string tokenId)
    {
        try
        {
            CartComplete result = await _cartBusiness.GetCompleteCart(tokenId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest("Opss.. aconteceu algum problema ao adicionar um produto em seu carrinho, tente novamente mais tarde!");
        }
    }

    [Authorize(Roles = "admin")]
    [HttpGet("v1/cart/all")]
    public async Task<IActionResult> GetallCartActive()
    {
        try
        {
            IEnumerable<CartModel> result = await _cartBusiness.GetAllActiveCart();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest("Opss.. aconteceu algum problema, tente novamente mais tarde!");
        }
    }

    [Authorize(Roles = "admin")]
    [HttpPost("v1/{tokenId}/delete")]
    public async Task<IActionResult> DeleteCart(string tokenId)
    {
        try
        {
            bool result = await _cartBusiness.DeleteCart(tokenId);

            if(result)
            {
                return Ok("Carrinho deletado com sucesso.");
            }
            else
            {
                return BadRequest("Você não possui autorização para deletar o carrinho.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Opss.. aconteceu algum problema ao deletar o carrinho, tente novamente mais tarde!");
        }
    }

    [Authorize(Roles = "admin")]
    [HttpPost("v1/{tokenId}/{itemId}/delete")]
    public async Task<IActionResult> DeleteItemCart(string tokenId, int itemId)
    {
        try
        {
            bool result = await _cartBusiness.DeleteItemCart(tokenId, itemId);

            if (result)
            {
                return Ok("Item deletado com sucesso no carrinho.");
            }
            else
            {
                return BadRequest("Você não possui autorização para deletar o item do carrinho.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Opss.. aconteceu algum problema ao deletar o item do carrinho, tente novamente mais tarde!");
        }
    }

    [Authorize(Roles = "admin")]
    [HttpPost("v1/update")]
    public async Task<IActionResult> updatestatusCart([FromBody] UpdateStatus update)
    {
        try
        {
            if (update.Status > 3)
                return BadRequest("Os status disponíveis são de 0 a 3, sendo 0 excluido, 1 ativo, 2 aguardando pagamento e 3 finalizado.");

            bool result = await _cartBusiness.UpdateStatusCart(update.Token, update.Status);

            if (result)
            {
                return Ok("Status alterado com sucesso.");
            }
            else
            {
                return BadRequest("Você não possui autorização para deletar o carrinho.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Opss.. aconteceu algum problema ao deletar o carrinho, tente novamente mais tarde!");
        }
    }
}