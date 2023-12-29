using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTransactions : ControllerBase
    {
        public readonly VbDbContext dbContext;

        public AccountTransactions(VbDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<AccountTransaction>> Get()
        {
            return await dbContext.Set<AccountTransaction>()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<AccountTransaction> GetById(int id)
        {
            var accountTransaction = await dbContext.Set<AccountTransaction>()
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return accountTransaction;
        }


        [HttpPost]
        public async Task Post([FromBody] AccountTransaction accountTransaction)
        {
            await dbContext.Set<AccountTransaction>().AddAsync(accountTransaction);
            await dbContext.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] AccountTransaction accountTransaction)
        {
            var fromdb = await dbContext.Set<AccountTransaction>().Where(x => x.Id == id).FirstOrDefaultAsync();
            fromdb.TransferType = accountTransaction.TransferType;
            fromdb.ReferenceNumber = accountTransaction.ReferenceNumber;
            fromdb.Description = accountTransaction.Description;
            fromdb.Amount = accountTransaction.Amount;

            await dbContext.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var fromdb = await dbContext.Set<AccountTransaction>().Where(x => x.Id == id).FirstOrDefaultAsync();
            fromdb.IsActive = false;
            await dbContext.SaveChangesAsync();
        }
    }
}
