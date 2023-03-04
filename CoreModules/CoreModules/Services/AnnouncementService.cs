using CoreModules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Services
{
    public class AnnouncementService
    {
        private readonly FixedFeeService _fixedFeeService;
        private readonly PublicFeeService _publicFeeService;
        private readonly PersonOweService _personOweService;
        private readonly SystemEnumService _systemEnumService;

        public AnnouncementService(
            FixedFeeService fixedFeeService,
            PublicFeeService publicFeeService,
            PersonOweService personOweService,
            SystemEnumService systemEnumService)
        {
            this._fixedFeeService = fixedFeeService;
            this._publicFeeService = publicFeeService;
            this._personOweService = personOweService;
            this._systemEnumService = systemEnumService;
        }

        public async Task<string> GetAnnouncementAsync()
        {
            var announcementModel = 
                await this._fixedFeeService.GetAnnouncementModelAsync(new AnnouncementModel());

            announcementModel =
                await this._publicFeeService.GetAnnouncementModelAsync(announcementModel);

            announcementModel =
                await this._personOweService.GetAnnouncementModelAsync(announcementModel);

            return announcementModel.Msg;
        }

        public async Task DeleteNoMatchSystemEnumAsync()
        {
            var systemEnums = await this._systemEnumService.GetAllAsync();

            var systemEnumNames = systemEnums.Select(x => x.Name).ToList();

            var fixedFees = await this._fixedFeeService.GetAllAsync();
            fixedFees.Where(f => !systemEnumNames.Contains(f.Name))
                .ToList()
                .ForEach(async (f) =>
                {
                    await this._fixedFeeService.DeleteAsync(f.GuId);
                });

            var publicFees = await this._publicFeeService.GetAllAsync();
            publicFees.Where(p => !systemEnumNames.Contains(p.Name))
                .ToList()
                .ForEach(async (p) =>
                {
                    await this._publicFeeService.DeleteAsync(p.GuId);
                });

            var personFees = await this._personOweService.GetAllAsync();
            personFees.Where(p => !systemEnumNames.Contains(p.DebitName) || !systemEnumNames.Contains(p.SideName))
                .ToList()
                .ForEach(async (p) =>
                {
                    await this._personOweService.DeleteAsync(p.GuId);
                });
        }
    }
}
