using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Models
{
    public class PublicFee : FeeBase
    {
        /// <summary>
        /// 名稱
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 名稱_英文
        /// </summary>
        public string? Name_En { get; set; }
    }
}
