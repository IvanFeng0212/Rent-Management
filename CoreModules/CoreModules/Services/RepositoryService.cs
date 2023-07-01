using CoreModules.Helpers;

namespace CoreModules.Services
{
    public abstract class RepositoryService
    {
        /// <summary>
        /// 取得指定 Table 所有資料
        /// </summary>
        protected async Task<List<T>> GetAllAsync<T>(string tableName)
        {
           return await FirebaseHelper.GetAllWithoutEmptyAsync<T>(tableName);
        }

        /// <summary>
        /// 新增指定 Table 資料
        /// </summary>
        public async Task AddAsync<T>(string tableName,T data)
        {
            await FirebaseHelper.InsertAsync<T>(tableName,data);
        }

        /// <summary>
        /// 刪除指定 Table 資料
        /// </summary>
        public async Task DeleteAsync<T>(string tableName, string itemId)
        {
            await FirebaseHelper.DeleteAsync<T>(tableName, itemId);
        }
    }
}
