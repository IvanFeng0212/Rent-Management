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

        public async Task<AnnouncementModel> GetAnnouncementModelAsync(AnnouncementModel announcementModel)
        {
            var publicFees = await base.GetAllAsync<PublicFee>(nameof(PublicFee));

            var strs = new List<string>();

            for (int i = 0; i < publicFees.Count; i += 1)
            {
                strs.Add($"{publicFees[i].Name} : {publicFees[i].Amount.ToString("N0")}");
                announcementModel.Amount += publicFees[i].Amount;
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
