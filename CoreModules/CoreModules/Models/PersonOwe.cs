namespace CoreModules.Models
{
    public class PersonOwe : FeeBase
    {
        /// <summary>
        /// 欠款人
        /// </summary>
        public string DebitName { get; set; }

        /// <summary>
        /// 收款人
        /// </summary>
        public string SideName { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { get; set; }
    }
}
