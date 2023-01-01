﻿using CoreModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Services
{
    public class SysEnumService : RepositoryService
    {
        private readonly Dictionary<string, string> sysEnumDic;
        public SysEnumService()
        {
            this.sysEnumDic = new Dictionary<string, string>()
            {
                { nameof(FixedFee),"固定費用"},
                { nameof(PublicFee),"公共費用" },
                { nameof(Person),"人員管理" }
            };
        }

        public List<SysEnum> GetItemList()
        {
           return this.sysEnumDic.Select(d => 
           {
                return new SysEnum() { ItemType = d.Key,ItemTypeName= d.Value};
           }).ToList();
        }

        public async Task<List<SysEnum>> GetAllAsync()
        {
            return await base.GetAllAsync<SysEnum>(nameof(SysEnum));
        }

        public async Task<List<SysEnum>> GetByTypeAsync(string type)
        {
            var sysEnums = await base.GetAllAsync<SysEnum>(nameof(SysEnum));

            return sysEnums.Where(s => s.ItemType == type).ToList();
        }

        public async Task AddAsync(SysEnum data)
        {
            var isRepeat = await this.CheckRepeat(data);

            if (isRepeat) return;

            await base.AddAsync<SysEnum>(nameof(SysEnum), data);
        }

        public async Task DeleteAsync(string itemId)
        {
            await base.DeleteAsync<SysEnum>(nameof(SysEnum), itemId);
        }

        private async Task<bool> CheckRepeat(SysEnum data)
        {
            var sysEnums = await this.GetAllAsync();

            return sysEnums.Any(s => s.Name == data.Name);
        }
    }
}
