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
    }
}
