using CoreModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Services
{
    public class PublicFeeService : RepositoryService
    {
        public async Task<List<PublicFee>> GetAllAsync()
        {
            return await base.GetAllAsync<PublicFee>(nameof(PublicFee));
        }

        public async Task AddAsync(PublicFee data)
        {
            await base.AddAsync<PublicFee>(nameof(PublicFee), data);
        }

        public async Task DeleteAsync(string itemId)
        {
            await base.DeleteAsync<PublicFee>(nameof(PublicFee),itemId);
        }
    }
}
