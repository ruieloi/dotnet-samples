using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace Data.Firebase.Interfaces
{
    public interface IFirebaseRepositoryBase
    {
        HttpClient Client { get; set; }
        string CollectioName { get; set; }
        Task<IEnumerable<T>> GetAll<T>() where T : IFirebaseEntity, new();
        Task<T> Insert<T>(T value) where T : IFirebaseEntity, new();
        Task<IEnumerable<T>> InsertList<T>(IEnumerable<T> values) where T : IFirebaseEntity, new();
        Task Delete<T>(T value) where T : IFirebaseEntity, new();
        Task Update<T>(T value) where T : IFirebaseEntity, new();
        Task<T> GetByKey<T>(string key) where T : IFirebaseEntity, new();       
    }
}