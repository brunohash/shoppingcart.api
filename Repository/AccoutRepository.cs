using ShoppingCart.Models;
using Dapper;
using MySqlConnector;
using ShoppingCart.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;

namespace ShoppingCart.Repository
{
	public class AccoutRepository : IAccoutRepository
    {
        private readonly IDbConnection _mySql;

        public AccoutRepository(IConfiguration config)
        {
            _mySql = new MySqlConnection(config.GetConnectionString("mySqlGeneral"));
        }

        public async Task<UserAuthenticate> ViewAuthenticate(string user, string pass)
        {
            try
            {
                return await _mySql.QueryFirstOrDefaultAsync<UserAuthenticate>(@"
                        SELECT Id, User, Role FROM `cartShopping`.`User.Api`
                        WHERE Status = 1 AND `User` = @User AND `Password` = @Pass
                      ",
                    new
                    {
                        User = user,
                        Pass = pass
                    });
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}

