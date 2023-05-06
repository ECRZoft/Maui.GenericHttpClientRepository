using MauiTodoApp.Models;

namespace MauiTodoApp.Services;
public interface ITodoService
{
    Task<List<TodoItem>> GetTasksAsync();
    Task<TodoItem> GetTaskByIdAsync(string id);
    Task SaveTaskAsync(TodoItem item, bool isNewItem);
    Task DeleteTaskAsync(TodoItem item);
}
