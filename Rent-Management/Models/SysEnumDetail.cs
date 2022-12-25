namespace Rent_Management.Models
{
    public class SysEnumDetail
    {
        /// <summary>
        /// 參數 Id
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 明細 Id
        /// </summary>
        public string GuId { get; set; }

        /// <summary>
        /// 明細名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public int Amount { get; set; }
    }
}
