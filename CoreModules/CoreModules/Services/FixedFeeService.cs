using CoreModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Services
{
    public class FixedFeeService : RepositoryService
    {
        public async Task<List<FixedFee>> GetAllAsync()
        {
            return await base.GetAllAsync<FixedFee>(nameof(FixedFee));
        }

        public async Task AddAsync(FixedFee data)
        {
            await base.AddAsync<FixedFee>(nameof(FixedFee), data);
        }

        public async Task DeleteAsync(string itemId)
        {
            await base.DeleteAsync<FixedFee>(nameof(FixedFee),itemId);
        }
    }
}
