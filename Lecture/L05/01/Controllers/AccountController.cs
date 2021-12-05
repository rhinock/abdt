using _01.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext context;

        public AccountController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetFromSql()
        {
            var accounts = await context.Accounts
                .FromSqlRaw(@"select * from ""Accounts"" where ""AccountNumber"" = '123'")
                .ToListAsync();

            return Ok(accounts);
        }

        [HttpGet("function")]
        public async Task<ActionResult> GetFunction()
        {
            var accounts = await context.Accounts
                .Include(x => x.Card)
                // .Where(x => x.AccountNumber.StartsWith("1"))
                .Where(x => EF.Functions.Like(x.AccountNumber, "1%"))
                .Select(x => new
                {
                    x.Card.PaymentSystem,
                    x.AccountNumber,
                    CardNumber = x.Card.Number
                })
                .ToListAsync();

            return Ok(accounts);
        }

        [HttpGet("in-memory")]
        public async Task<ActionResult> GetInMemory()
        {
            var accounts = await context.Accounts
                .Select(x => new
                {
                    x.AccountNumber,
                    GetSymbol = GetSymbol(x.AccountNumber)
                })
                .ToListAsync();

            return Ok(accounts);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <returns></returns>
        //[HttpGet("in-memory-filter")]
        //public async Task<ActionResult> GetInMemoryWithFilter()
        //{
        //    var accounts = await context.Accounts
        //        .Where(x => GetSymbol(x.AccountNumber) == "P.")
        //        .ToListAsync();

        //    return Ok(accounts);
        //}

        [HttpGet("in-memory-filter")]
        public IEnumerable<Account> GetInMemoryWithFilter()
        {
            var accounts = context
                .Set<Account>()
                .AsEnumerable()
                .Where(x => GetSymbol(x.AccountNumber) == "P.")
                .ToList();

            return accounts;
        }

        private static string GetSymbol(string currency)
        {
            if (currency.ToLower().StartsWith("ru"))
                return "P.";

            return currency.ToLower() switch
            {
                "usd" => "$.",
                "eur" => "Eu.",
                _ => "P."
            };
        }
    }
}
