using CoreModules.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreModules.Models;

namespace CoreModules.Services
{
    public class JsonService : IJsonService
    {
        private readonly string cryptoKey = Environment.GetEnvironmentVariable("RM_SecretKey");

        /// <summary>
        /// 取得指定 Json 檔所有資料
        /// </summary>
        public async Task<T> GetAll<T>(string jsonFileName) where T : class
        {
            var filePath = GetJsonFilePath(jsonFileName);

            if (!File.Exists(filePath))
            {
                return default(T);
            }

            using (var streamReader = new StreamReader(filePath))
            {
                string jsonString = await streamReader.ReadToEndAsync();

                var jsonBaseModel = JsonConvert.DeserializeObject<JsonBaseModel>(jsonString);

                return JsonConvert.DeserializeObject<T>(jsonBaseModel.EncryptString.DecryptString(cryptoKey));
            }
        }

        /// <summary>
        /// 儲存指定 Json 檔資料
        /// </summary>
        public async Task Save(string jsonFileName,object saveData)
        {
            var filePath = GetJsonFilePath(jsonFileName);
            if (!File.Exists(filePath)) return;

            var JsonBaseModel = new JsonBaseModel()
            {
                EncryptString = JsonConvert.SerializeObject(saveData).EncryptString(cryptoKey)
            };

            File.WriteAllText(filePath, JsonConvert.SerializeObject(JsonBaseModel));
        }

        private string GetJsonFilePath(string jsonFileName)
        {
            return Path.Combine(Environment.CurrentDirectory, "Datas", $"{jsonFileName}.json");
        }
    }
}
