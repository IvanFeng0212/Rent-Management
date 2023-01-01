using System.ComponentModel.DataAnnotations;

namespace CoreModules.Models
{
    public class FeeBase
    {
        /// <summary>
        /// Id
        /// </summary>
        public string? GuId { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [Range(1,30000)]
        public int Amount { get; set; }
    }
}
