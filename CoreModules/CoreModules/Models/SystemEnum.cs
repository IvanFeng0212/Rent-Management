using System.ComponentModel.DataAnnotations;

namespace CoreModules.Models
{
    public class SystemEnum
    {
        /// <summary>
        /// Id
        /// </summary>
        public string? GuId { get; set; }

        /// <summary>
        /// 參數項目
        /// </summary>
        [Required]
        public string ItemType { get; set; }

        /// <summary>
        /// 參數項目名稱
        /// </summary>
        [Required]
        public string ItemTypeName { get; set; }

        /// <summary>
        /// 參數名稱
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
