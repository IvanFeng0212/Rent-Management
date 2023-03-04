using CoreModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Services
{
    public class SystemEnumService : RepositoryService
    {
        private readonly Dictionary<string, string> sysEnumDic;
        public SystemEnumService()
        {
            this.sysEnumDic = new Dictionary<string, string>()
            {
                { nameof(FixedFee),"固定費用"},
                { nameof(PublicFee),"公共費用" },
                { nameof(PersonOwe),"人員管理" }
            };
        }

        public List<SystemEnum> GetItemList()
        {
           return this.sysEnumDic.Select(d => 
           {
                return new SystemEnum() { ItemType = d.Key,ItemTypeName= d.Value};
           }).ToList();
        }

        public async Task<List<SystemEnum>> GetAllAsync()
        {
            return await base.GetAllAsync<SystemEnum>(nameof(SystemEnum));
        }

        public async Task<List<SystemEnum>> GetByTypeAsync(string type)
        {
            var sysEnums = await base.GetAllAsync<SystemEnum>(nameof(SystemEnum));

            return sysEnums.Where(s => s.ItemType == type).ToList();
        }

        public async Task AddAsync(SystemEnum data)
        {
            var isRepeat = await this.CheckRepeat(data);

            if (isRepeat) return;

            await base.AddAsync<SystemEnum>(nameof(SystemEnum), data);
        }

        public async Task DeleteAsync(string itemId)
        {
            await base.DeleteAsync<SystemEnum>(nameof(SystemEnum), itemId);
        }

        private async Task<bool> CheckRepeat(SystemEnum data)
        {
            var sysEnums = await this.GetAllAsync();

            return sysEnums.Any(s => s.Name == data.Name);
        }
    }
}
