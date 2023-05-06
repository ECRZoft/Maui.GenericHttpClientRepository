# .NET MAUI GenericHttpClientRepository

`GenericHttpClientRepository` is a simple, generic repository for .NET MAUI applications, that communicates with a REST API.

It implements the following interface:

```csharp
    Task<T> GetAsync<T>(Uri uri, string authToken = "");
    Task PostAsync<T>(Uri uri, T data, string authToken = "");
    Task<R> PostAsync<T, R>(Uri uri, T data, string authToken = "");
    Task<T> PutAsync<T>(Uri uri, T data, string authToken = "");
    Task DeleteAsync(Uri uri, string authToken = "");
```

&nbsp;

## Getting Started

In order to use GenericHttpClientRepository you need to register the library in MauiProgram.cs:

```csharp
builder.Services.AddGenericHttpClientRepository();
```

The methods in the library are supposed to be called from one or more service classes, that are typed with the actual domain class. 
An example from the Todo example is shown here:


**ITodoService.cs**
```csharp
public interface ITodoService
{
    Task<List<TodoItem>> GetTasksAsync();
    Task<TodoItem> GetTaskByIdAsync(string id);
    Task SaveTaskAsync(TodoItem item, bool isNewItem);
    Task DeleteTaskAsync(TodoItem item);
}
```

**TodoService.cs**
```csharp
public class TodoService : ITodoService
{
    private readonly IGenericRepository _repository;

    public TodoService(IGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TodoItem>> GetTasksAsync()
    {
        Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
        return await _repository.GetAsync<List<TodoItem>>(uri);
    }

    public async Task<TodoItem> GetTaskByIdAsync(string id)
    {
        Uri uri = new Uri(string.Format(Constants.RestUrl, id));
        return await _repository.GetAsync<TodoItem>(uri);
    }

    public async Task SaveTaskAsync(TodoItem item, bool isNewItem = false)
    {
        if (isNewItem)
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
            await _repository.PostAsync(uri, item);
        }
        else
        {
            Uri uri = new Uri(string.Format(Constants.RestUrl, item.Id));
            await _repository.PutAsync(uri, item);
        }
    }

    public async Task DeleteTaskAsync(TodoItem item)
    {
        Uri uri = new Uri(string.Format(Constants.RestUrl, item.Id));
        await _repository.DeleteAsync(uri);
    }
}
```
&nbsp;

The `Constant.RestUrl` comes from a configuration file, *Constants.cs*:

```csharp
public static class Constants
{
    // Dev Tunnel for development test
    public static string RestUrl = $"https://<Base address or Dev Tunnel>/todoitems/{{0}}";
}
```
&nbsp;
> **Remark: It is supposed that the WebApi is either deployed to an webserver with valid TLS certificate or to Microsofts Dev Tunnel.**

&nbsp;

### Dependencies

- Microsoft.Extensions.DependencyInjection (>= 7.0.0)
- Microsoft.Extensions.Logging.Abstractions (>= 7.0.0)
- Polly (>= 7.2.3)