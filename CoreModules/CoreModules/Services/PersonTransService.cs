using CoreModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Services
{
    public class PersonTransService : RepositoryService
    {
        public async Task<List<Person>> GetAllAsync()
        {
            return await base.GetAllAsync<Person>(nameof(Person));
        }

        public async Task AddAsync(Person data)
        {
            await base.AddAsync<Person>(nameof(Person), data);
        }

        public async Task DeleteAsync(string itemId)
        {
            await base.DeleteAsync<Person>(nameof(Person),itemId);
        }
    }
}
