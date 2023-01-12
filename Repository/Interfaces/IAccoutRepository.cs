using System;
using ShoppingCart.Models;

namespace ShoppingCart.Repository.Interfaces
{
	public interface IAccoutRepository
    {
        Task<UserAuthenticate> ViewAuthenticate(string user, string pass);
    }
}

