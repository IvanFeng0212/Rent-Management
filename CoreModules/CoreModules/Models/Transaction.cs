namespace CoreModules.Models
{
    public class Transaction
    {
        /// <summary>
        /// 借款人 Id
        /// </summary>
        public string Debit { get; set; }

        /// <summary>
        /// 收款人 Id
        /// </summary>
        public string Side { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 註記
        /// </summary>
        public string Remark { get; set; }
    }

}
