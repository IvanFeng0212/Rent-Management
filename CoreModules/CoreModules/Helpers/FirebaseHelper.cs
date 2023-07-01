using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Helpers
{
    public class FirebaseHelper
    {
        public static async Task<List<T>> GetAllWithoutEmptyAsync<T>(string tableName)
        {
            return (await GetAllAsync<T>(tableName)).Where(x => x != null).ToList();
        }

        public static async Task<List<T>> GetAllAsync<T>(string tableName)
        {
            try
            {
                using (var firebaseClient = GetFirebaseClient())
                {
                    var table = firebaseClient.Child(tableName);

                    var firebaseObjects = await table.OnceAsListAsync<T>();

                    var jsonContent = JsonConvert.SerializeObject(firebaseObjects.Select(f => f.Object));

                    return JsonConvert.DeserializeObject<List<T>>(jsonContent) ?? new List<T>();
                }
            }
            catch
            {
                return new List<T>();
            }
        }

        public static async Task InsertAsync<T>(string tableName, T insertData)
        {
            if (insertData == null) return;

            try
            {
                using (var firebaseClient = GetFirebaseClient())
                {
                    insertData.GetType().GetProperty("GuId").SetValue(insertData, Guid.NewGuid().ToString());

                    var datas = await GetAllWithoutEmptyAsync<T>(tableName);

                    await firebaseClient.Child(tableName).Child(datas.Count.ToString()).PutAsync(insertData);
                }
            }
            finally
            {
            }
        }

        public static async Task DeleteAsync<T>(string tableName, string itemId)
        {
            try
            {
                using (var firebaseClient = GetFirebaseClient())
                {
                    var datas = await GetAllAsync<T>(tableName);

                    var dataIndex = datas
                        .FindIndex(d => d != null && d.GetType().GetProperty("GuId").GetValue(d).ToString() == itemId);

                    if (dataIndex == -1) return;

                    var childNode = $"{tableName}/{dataIndex}";

                    // Delete the child node
                    await firebaseClient.Child(childNode).DeleteAsync();
                }
            }
            finally
            {
            }
        }

        private static FirebaseClient GetFirebaseClient()
        {
            return new FirebaseClient(EnvironmentHelper.GetFirebaseUrl(),
            new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(EnvironmentHelper.GetFirebaseAuthKey())
            });
        }
    }
}