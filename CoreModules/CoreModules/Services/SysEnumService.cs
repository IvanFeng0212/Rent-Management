using CoreModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Services
{
    public class SysEnumService : RepositoryService
    {
        public async Task<List<SysEnum>> GetByType(string type)
        {
            var sysEnums = await base.GetAllAsync<SysEnum>(nameof(SysEnum));

            return sysEnums.Where(s => s.ItemType == type).ToList();
        }
    }
}
