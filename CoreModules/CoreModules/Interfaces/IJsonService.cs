using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Interfaces
{
    /// <summary>
    /// Json 讀寫介面
    /// </summary>
    public interface IJsonService
    {
        /// <summary>
        /// 取得指定 Json 檔所有資料
        /// </summary>
        Task<T> GetAll<T>(string jsonFileName) where T : class;

        /// <summary>
        /// 儲存指定 Json 檔資料
        /// </summary>
        /// <param name="jsonFileName"></param>
        Task Save(string jsonFileName, object saveData);
    }
}
