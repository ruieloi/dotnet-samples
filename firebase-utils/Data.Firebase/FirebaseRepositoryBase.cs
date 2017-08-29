using Newtonsoft.Json;
using Data.Firebase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Data.Firebase
{
    public class FirebaseRepositoryBase : IFirebaseRepositoryBase
    {
        #region Properties
        
        public HttpClient Client { get; set; }
        public string CollectioName { get; set; }

        private readonly string _appsecret; //Firebase APP Secret
        private readonly string _serverURL; //Firebase Server url ex: https://xxxxxxxxxxxx.firebaseio.com

        #endregion

        public FirebaseRepositoryBase(string firebaseServerURL, string firebaseAppSecret, string collectioName)
        {
            this._appsecret = firebaseAppSecret;
            this._serverURL = firebaseServerURL;

            CollectioName = collectioName;

            Client = new HttpClient()
            {
                BaseAddress = new Uri(_serverURL)
            };
        }
                
        #region Get

        public async Task<IEnumerable<T>> GetAll<T>() 
            where T : IFirebaseEntity, new()
        {
            var scheduledEvents = await this.Client.GetStringAsync($"/{CollectioName}/.json?auth={_appsecret}");
            var result = JsonConvert.DeserializeObject<Dictionary<string, T>>(scheduledEvents);

            return ConvertDictionaryToIEnumerable(result);
        }

        public async Task<T> GetByKey<T>(string key) 
            where T : IFirebaseEntity, new()
        {
            var value = await Client.GetStringAsync($"/{CollectioName}/{key}.json?auth={_appsecret}");
            return JsonConvert.DeserializeObject<T>(value);
        }
        #endregion


        #region Post

        public async Task<T> Insert<T>(T value) where T : IFirebaseEntity, new()
        {
            var jsonInString = JsonConvert.SerializeObject(value);
            var result = await Client.PostAsync($"/{CollectioName}/.json?auth={_appsecret}", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                var postResult = JsonConvert.DeserializeObject<FireabasePostResultBase>(await result.Content.ReadAsStringAsync());
                value.Key = postResult.Name;
                return value;
            }

            return default(T);
        }

        public async Task<IEnumerable<T>> InsertList<T>(IEnumerable<T> values) where T : IFirebaseEntity, new()
        {
            foreach (var item in values)
            {
                var jsonInString = JsonConvert.SerializeObject(item);
                var result = await Client.PostAsync($"/{CollectioName}/.json?auth={_appsecret}", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                if (result.IsSuccessStatusCode)
                {
                    var postResult = JsonConvert.DeserializeObject<FireabasePostResultBase>(await result.Content.ReadAsStringAsync());
                    item.Key = postResult.Name;
                }
            }
            return values;
        }

        #endregion

        #region Put

        public async Task Update<T>(T value) where T : IFirebaseEntity, new()
        {
            var jsonInString = JsonConvert.SerializeObject(value);
            var result = await Client.PutAsync($"/{CollectioName}/{value.Key}.json?auth={_appsecret}", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            //TODO Validate result
        }

        #endregion

        #region Delete

        public async Task Delete<T>(T value) where T : IFirebaseEntity, new()
        {
            var result = await Client.DeleteAsync($"/{CollectioName}/{value.Key}.json?auth={_appsecret}");
        }

        public async Task ClearCollection()
        {
            var result = await Client.DeleteAsync($"/{CollectioName}/.json?auth={_appsecret}");
        }
        #endregion

        #region Private Functions

        
        public static IEnumerable<T> ConvertDictionaryToIEnumerable<T>(Dictionary<string, T> dictionary)
             where T : IFirebaseEntity, new()
        {
            if (dictionary == null) return null;

            return dictionary.Select(x =>
            {
                x.Value.Key = x.Key;
                return x.Value;
            });
        }

        #endregion


    }
}