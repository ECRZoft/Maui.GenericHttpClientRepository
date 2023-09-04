namespace GenericHttpClientRepository;
public interface IGenericRepository
{
    Task<T?> GetAsync<T>(Uri uri, string authToken = "");
    Task PostAsync<T>(Uri uri, T data, string authToken = "");
    Task<R> PostAsync<T, R>(Uri uri, T data, string authToken = "");
    Task<T> PutAsync<T>(Uri uri, T data, string authToken = "");
    Task DeleteAsync(Uri uri, string authToken = "");
}
