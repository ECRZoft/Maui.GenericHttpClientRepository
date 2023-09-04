using GenericHttpClientRepository.Policies;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GenericHttpClientRepository;
public class GenericRepository : IGenericRepository
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;
    private readonly ILogger<GenericRepository> _logger;
    private readonly ClientPolicy _clientPolicy;

    #region CONSTRUCTOR
    public GenericRepository(ILogger<GenericRepository> logger, ClientPolicy clientPolicy)
    {
        _client = new HttpClient();

        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        _logger = logger;
        _clientPolicy = clientPolicy;
    }
    #endregion

    #region GET
    public async Task<T?> GetAsync<T>(Uri uri, string authToken = "")
    {
        ConfigureHttpClient(authToken);

        T? result = default;

        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    Debug.WriteLine(@"+++++ No string to deserialize");
                }
                else
                {
                    result = JsonSerializer.Deserialize<T>(content, _serializerOptions);
                    Debug.WriteLine(@"+++++ Item(s) successfully received.");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"----- ERROR {0}", ex.Message);
        }
        return result;
    }
    #endregion

    #region POST
    public async Task PostAsync<T>(Uri uri, T data, string authToken = "")
    {
        try
        {
            ConfigureHttpClient(authToken);

            string json = JsonSerializer.Serialize(data, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await _client.PostAsync(uri, content);
            responseMessage.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR in PostAsync() {ex.Message}");
            throw;
        }
    }

    public async Task<R> PostAsync<T, R>(Uri uri, T data, string authToken = "")
    {
        ConfigureHttpClient(authToken);
        R? items = default;
        try
        {
            string json = JsonSerializer.Serialize(data, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                items = JsonSerializer.Deserialize<R>(jsonContent, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR in PostAsync<T, TR>() {ex.Message}");
            throw;
        }
        return items!;
    }
    #endregion

    #region PUT
    public async Task<T> PutAsync<T>(Uri uri, T data, string authToken = "")
    {
        ConfigureHttpClient(authToken);
        T? item = default;
        try
        {
            string json = JsonSerializer.Serialize(data, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync(uri, content);

            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR in PutAsync() {ex.Message}");
            throw;
        }
        return item!;
    }
    #endregion

    #region DELETE
    public async Task DeleteAsync(Uri uri, string authToken = "")
    {
        ConfigureHttpClient(authToken);
        try
        {
            HttpResponseMessage response = await _client.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
        }

        catch (Exception ex)
        {
            _logger.LogError($"ERROR in DeleteAsync() {ex.Message}");
            throw;
        }
    }
    #endregion

    #region HELPER
    private void ConfigureHttpClient(string authToken)
    {
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (!string.IsNullOrEmpty(authToken))
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }
        else
        {
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
    #endregion
}
