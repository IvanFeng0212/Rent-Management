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
        private readonly PersonTransService _personTransService;
        private readonly SysEnumService _sysEnumService;

        public AnnouncementService(
            FixedFeeService fixedFeeService,
            PublicFeeService publicFeeService,
            PersonTransService personTransService,
            SysEnumService sysEnumService)
        {
            this._fixedFeeService = fixedFeeService;
            this._publicFeeService = publicFeeService;
            this._personTransService = personTransService;
            this._sysEnumService = sysEnumService;
        }

        public async Task<string> GetAnnouncementAsync()
        {
            var announcementModel = 
                await this._fixedFeeService.GetAnnouncementModelAsync(new AnnouncementModel());

            announcementModel =
                await this._publicFeeService.GetAnnouncementModelAsync(announcementModel);

            announcementModel =
                await this._personTransService.GetAnnouncementModelAsync(announcementModel);

            return announcementModel.Msg;
        }

        public async Task DeleteNoMatchSysEnumAsync()
        {
            var sysEnums = await this._sysEnumService.GetAllAsync();

            var sysEnumNames = sysEnums.Select(x => x.Name).ToList();

            var fixedFees = await this._fixedFeeService.GetAllAsync();
            fixedFees.Where(f => !sysEnumNames.Contains(f.Name))
                .ToList()
                .ForEach(async (f) =>
                {
                    await this._fixedFeeService.DeleteAsync(f.GuId);
                });

            var publicFees = await this._publicFeeService.GetAllAsync();
            publicFees.Where(p => !sysEnumNames.Contains(p.Name))
                .ToList()
                .ForEach(async (p) =>
                {
                    await this._publicFeeService.DeleteAsync(p.GuId);
                });

            var personFees = await this._personTransService.GetAllAsync();
            personFees.Where(p => !sysEnumNames.Contains(p.DebitName) || !sysEnumNames.Contains(p.SideName))
                .ToList()
                .ForEach(async (p) =>
                {
                    await this._personTransService.DeleteAsync(p.GuId);
                });
        }
    }
}
