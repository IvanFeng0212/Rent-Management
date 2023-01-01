using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Models
{
    public class PersonViewModel
    {
        public List<Person> PersonFees { get; set; }

        public List<SysEnum> PersonItems { get; set; }
    }
}
