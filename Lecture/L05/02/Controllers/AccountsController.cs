using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _02.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext context;

        public AccountsController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("sql")]
        public async Task<ActionResult<List<Account>>> GetFromSql()
        {
            var accounts = await context.Accounts
                .FromSqlRaw(@"select * from ""Accounts"" where ""Number"" ='string'")
                .ToListAsync();

            return Ok(accounts);
        }

        [HttpGet("query")]
        public async Task<ActionResult> GetLinq()
        {
            var query = context
                .Accounts
                .Where(x => x.Number != "")
                .Select(x => new
                {
                    x.Currency,
                    x.Number
                });

            var accounts = await query.ToListAsync();
            return Ok(accounts);
        }

        [HttpGet("function")]
        public async Task<ActionResult> GetFunction()
        {
            var query = context
                .Accounts
                .Where(x => !string.IsNullOrEmpty(x.Number))
                //.Where(x => x.Currency.ToLower().StartsWith("ru"))
                .Where(x => EF.Functions.Like(x.Currency.ToLower(), "ru%"))
                .Select(x => new
                {
                    x.Currency,
                    x.Number
                });

            var accounts = await query.ToListAsync();
            return Ok(accounts);
        }

        [HttpGet("in-memory")]
        public async Task<ActionResult> GetInMemory()
        {
            var accounts = await context.Accounts
                .Select(x => new
                {
                    x.Currency,
                    Symbol = GetSymbol(x.Currency)
                })
                .ToListAsync();

            return Ok(accounts);
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

        [HttpGet("in-memory-filter")]
        public async Task<ActionResult> FilterInMemory()
        {
            var accounts = context.Accounts
                .AsEnumerable()
                .Where(x => GetSymbol(x.Currency) == "$.")
                .ToList();
            // .ToListAsync();

            return Ok(accounts);
        }

        [HttpGet("include")]
        public async Task<ActionResult> GetWithInclude()
        {
            var accounts = await context.Accounts
                .Include(x => x.Card)
                .Select(x => new
                {
                    x.Currency,
                    CardNumber = x.Card.Number
                })
                .ToListAsync();

            return Ok(accounts);
        }
    }
}