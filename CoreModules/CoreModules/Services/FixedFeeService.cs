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

        public async Task<AnnouncementModel> GetAnnouncementModelAsync(AnnouncementModel announcementModel)
        {
            var fixedFees = await base.GetAllAsync<FixedFee>(nameof(FixedFee));

            var strs = new List<string>();

            for(int i = 0; i < fixedFees.Count; i += 1)
            {
                strs.Add($"{fixedFees[i].Name} : {fixedFees[i].Amount.ToString("N0")}");
                announcementModel.Amount += fixedFees[i].Amount;
            }

            if (!string.IsNullOrEmpty(announcementModel.Msg))
            {
                strs.Insert(0, announcementModel.Msg);
            }

            announcementModel.Msg = string.Join(" + ", strs);

            return announcementModel;
        }
    }
}
