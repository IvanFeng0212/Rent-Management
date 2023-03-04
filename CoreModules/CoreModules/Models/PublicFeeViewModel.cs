using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Models
{
    public class PublicFeeViewModel
    {
        public List<PublicFee> PublicFees { get; set; }

        public List<SystemEnum> PublicFeeItems { get; set; }
    }
}
