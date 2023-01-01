using Newtonsoft.Json;
using CoreModules.Models;
using System.Data;

namespace CoreModules.Services
{
    public abstract class RepositoryService
    {
        private readonly string cryptoKey = Environment.GetEnvironmentVariable("RM_SecretKey");

        /// <summary>
        /// 取得指定 Json 檔所有資料
        /// </summary>
        protected async Task<List<T>> GetAllAsync<T>(string jsonFileName)
        {
            var filePath = GetJsonFilePath(jsonFileName);

            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            using (var streamReader = new StreamReader(filePath))
            {
                string jsonString = await streamReader.ReadToEndAsync();

                var jsonBaseModel = JsonConvert.DeserializeObject<JsonEncryptModel>(jsonString);

                var dataBaseModel = JsonConvert.DeserializeObject<DataBaseModel<T>>(jsonBaseModel.EncryptString.DecryptString(cryptoKey));

                return dataBaseModel.Data;
            }
        }

        /// <summary>
        /// 新增指定 Json 檔資料
        /// </summary>
        public async Task AddAsync<T>(string jsonFileName,T data)
        {
            if (data == null) return;

            data.GetType().GetProperty("GuId").SetValue(data, Guid.NewGuid().ToString());

            var datas = await this.GetAllAsync<T>(jsonFileName);
           
            datas.Add(data);

            await this.SaveAsync(jsonFileName, datas);
        }

        /// <summary>
        /// 刪除指定 Json 檔資料
        /// </summary>
        public async Task DeleteAsync<T>(string jsonFileName, string itemId)
        {
            var datas = await this.GetAllAsync<T>(jsonFileName);

            var data = datas
                .Where(d => d.GetType().GetProperty("GuId").GetValue(d).ToString() == itemId)
                .FirstOrDefault();

            if (data == null) return;

            datas.Remove(data);

            await this.SaveAsync(jsonFileName,datas);
        }

        private string GetJsonFilePath(string jsonFileName)
        {
            var dirPath = Path.Combine(Environment.CurrentDirectory, "Datas");

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            return Path.Combine(dirPath, $"{jsonFileName}.json");
        }

        private async Task SaveAsync<T>(string jsonFileName,List<T> datas)
        {
            var filePath = GetJsonFilePath(jsonFileName);

            var dataBaseModel = new DataBaseModel<T>()
            {
                Data = datas
            };

            var saveData = new JsonEncryptModel()
            {
                EncryptString = 
                JsonConvert.SerializeObject(dataBaseModel).EncryptString(cryptoKey),
            };

            await File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(saveData));
        }
    }
}
