using CoreModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoreModules.Services
{
    public class PersonTransService : RepositoryService
    {
        private readonly Dictionary<string, int> personRentDic;
        private readonly Dictionary<string, string> personMsgDic;
        private readonly HashSet<string> personDic;
        private readonly SysEnumService _sysEnumService;

        public PersonTransService(SysEnumService sysEnumService)
        {
            personRentDic = new Dictionary<string, int>();
            personMsgDic = new Dictionary<string, string>();
            personDic = new HashSet<string>();
            this._sysEnumService = sysEnumService;
        }

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

        public async Task<AnnouncementModel> GetAnnouncementModelAsync(AnnouncementModel announcementModel)
        {
            var avgFee =
                await this.GivenPersonDicAsync(announcementModel.Amount);

            await UpdatePersonDic();

            announcementModel.Msg = this.CombinMsg(announcementModel, avgFee);

            return announcementModel;
        }

        private async Task<int> GivenPersonDicAsync(int totalFee)
        {
            var personSysEnums = await this._sysEnumService.GetByTypeAsync(nameof(Person));

            var avgFee = 
                (int)Math.Floor(Convert.ToDecimal(totalFee / personSysEnums.Count));

            personSysEnums.ForEach(p =>
            {
                personDic.Add(p.Name);
                personRentDic.TryAdd(p.Name, avgFee);
                personMsgDic.TryAdd(p.Name, $"基本 {avgFee.ToString("N0")}");
            });

            return avgFee;
        }

        private async Task UpdatePersonDic()
        {
            var persons = await base.GetAllAsync<Person>(nameof(Person));

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

            // 管理費 : xx,xxx + 房租 : xxxx + 水費 : xxx = xx,xxx
            stringBuilder.AppendLine($"{announcementModel.Msg} = {announcementModel.Amount.ToString("N0")}");

            // 基本: xx,xxx / x = x,xxx
            stringBuilder.AppendLine($"基本 : {announcementModel.Amount} / {personDic.Count} = {avgFee.ToString("N0")}");

            stringBuilder.AppendLine("----------------------------------------");

            // P : 基本 x,xxx (+/- 人員欠債費用) = x,xxx
            foreach (var personName in personDic)
            {
                stringBuilder.AppendLine($"{personName} : {personMsgDic[personName]} = {personRentDic[personName].ToString("N0")}");
            }

            return stringBuilder.ToString();
        }
    }
}
