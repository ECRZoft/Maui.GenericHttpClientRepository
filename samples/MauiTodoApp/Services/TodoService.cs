using GenericHttpClientRepository;
using MauiTodoApp.Models;

namespace MauiTodoApp.Services;
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