using CoreModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoreModules.Services
{
    public class PersonOweService : RepositoryService
    {
        private readonly Dictionary<string, int> personRentDic;
        private readonly Dictionary<string, string> personMsgDic;
        private readonly HashSet<string> personDic;
        private readonly SystemEnumService _systemEnumService;

        public PersonOweService(SystemEnumService systemEnumService)
        {
            personRentDic = new Dictionary<string, int>();
            personMsgDic = new Dictionary<string, string>();
            personDic = new HashSet<string>();
            this._systemEnumService = systemEnumService;
        }

        public async Task<List<PersonOwe>> GetAllAsync()
        {
            return await base.GetAllAsync<PersonOwe>(nameof(PersonOwe));
        }

        public async Task AddAsync(PersonOwe data)
        {
            await base.AddAsync<PersonOwe>(nameof(PersonOwe), data);
        }

        public async Task DeleteAsync(string itemId)
        {
            await base.DeleteAsync<PersonOwe>(nameof(PersonOwe),itemId);
        }

        public async Task<AnnouncementModel> GetAnnouncementModelAsync(AnnouncementModel announcementModel)
        {
            var personOwes = await this._systemEnumService.GetByTypeAsync(nameof(PersonOwe));

            var avgFee = this.GetAvgFee(announcementModel.Amount, personOwes);

            SetPersonDic(avgFee, personOwes);

            if (personDic.Count == 0) return announcementModel;

            await UpdatePersonDic();

            announcementModel.Msg = this.CombinMsg(announcementModel, avgFee);

            return announcementModel;
        }

        private int GetAvgFee(int totalFee, List<SystemEnum> personOwes)
        {
            if (personOwes.Count == 0) return 0;

            return (int)Math.Floor(Convert.ToDecimal(totalFee / personOwes.Count));
        }

        private void SetPersonDic(int avgFee, List<SystemEnum> personOwes) 
        {
            personOwes.ForEach(p =>
            {
                personDic.Add(p.Name);
                personRentDic.TryAdd(p.Name, avgFee);
                personMsgDic.TryAdd(p.Name, $"基本 {avgFee.ToString("N0")}");
            });
        }

        private async Task UpdatePersonDic()
        {
            var persons = await base.GetAllAsync<PersonOwe>(nameof(PersonOwe));

            persons.ForEach(p =>
            {
                personRentDic[p.DebitName] += p.Amount;
                personMsgDic[p.DebitName] += $" + {p.Remark} : {p.Amount.ToString("N0")}";

                personRentDic[p.SideName] -= p.Amount;
                personMsgDic[p.SideName] += $" - {p.Remark} : {p.Amount.ToString("N0")}";
            });
        }

        private string CombinMsg(AnnouncementModel announcementModel,int avgFee)
        {
            var stringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(announcementModel.Msg))
            {
                // 管理費 : xx,xxx + 房租 : xxxx + 水費 : xxx = xx,xxx
                stringBuilder.AppendLine($"{announcementModel.Msg} = {announcementModel.Amount.ToString("N0")}");

                // 基本: xx,xxx / x = x,xxx
                stringBuilder.AppendLine($"基本 : {announcementModel.Amount.ToString("N0")} / {personDic.Count} = {avgFee.ToString("N0")}");

                stringBuilder.AppendLine("----------------------------------------");
            }

            // P : 基本 x,xxx (+/- 人員欠債費用) = x,xxx
            foreach (var personName in personDic)
            {
                stringBuilder.AppendLine($"{personName} : {personMsgDic[personName]} = {personRentDic[personName].ToString("N0")}");
            }

            return stringBuilder.ToString();
        }
    }
}
