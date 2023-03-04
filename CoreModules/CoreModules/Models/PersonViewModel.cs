using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Models
{
    public class PersonViewModel
    {
        public List<PersonOwe> PersonFees { get; set; }

        public List<SystemEnum> PersonItems { get; set; }
    }
}
