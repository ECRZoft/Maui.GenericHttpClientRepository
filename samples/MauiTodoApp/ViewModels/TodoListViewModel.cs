using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTodoApp.Models;
using MauiTodoApp.Services;
using MauiTodoApp.Views;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace MauiTodoApp.ViewModels;

public partial class TodoListViewModel : BaseViewModel
{
    public ObservableCollection<TodoItem> TodoItems { get; } = new();

    readonly ITodoService service;
    private readonly IConnectivity connectivity;
    private readonly ILogger<TodoListViewModel> logger;

    public TodoListViewModel(ITodoService service, IConnectivity connectivity, ILogger<TodoListViewModel> logger)
    {
        this.service = service;
        this.connectivity = connectivity;
        this.logger = logger;
    }

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async Task GetItemsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            IsBusy = true;
            List<TodoItem> todos = await service.GetTasksAsync();

            if (TodoItems.Count != 0)
                TodoItems.Clear();

            foreach (var todoitem in todos)
                TodoItems.Add(todoitem);

        }
        catch (Exception ex)
        {
            logger.LogError($"***** Unable to get TodoItems: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }


    [RelayCommand]
    async Task AddItem()
    {
        await Shell.Current.GoToAsync(nameof(TodoItemPage), true, new Dictionary<string, object>
        {
            {"item", new TodoItem() }
        });
    }

    [RelayCommand]
    async Task GoToDetails(TodoItem todoItem)
    {
        if (todoItem == null)
            return;

        await Shell.Current.GoToAsync(nameof(TodoItemPage), new Dictionary<string, object>
        {
            {"item", todoItem }
        });
    }
}
