using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Models
{
    public class FixedFeeViewModel
    {
        public List<FixedFee> FixedFees { get; set; }

        public List<SystemEnum> FixedFeeItems { get; set; }
    }
}
