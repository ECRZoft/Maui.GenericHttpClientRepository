using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTodoApp.Models;
using MauiTodoApp.Services;

namespace MauiTodoApp.ViewModels;

[QueryProperty(nameof(TodoItem), "item")]
public partial class TodoItemViewModel : BaseViewModel
{
    readonly ITodoService _todoService;
    public TodoItemViewModel(ITodoService service)
    {
        _todoService = service;
    }

    [ObservableProperty]
    TodoItem todoItem;

    bool isNewItem;

    partial void OnTodoItemChanging(TodoItem value)
    {
        isNewItem = string.IsNullOrWhiteSpace(value.Name) && string.IsNullOrWhiteSpace(value.Notes) ? true : false;
    }

    [RelayCommand]
    async Task Save()
    {
        await _todoService.SaveTaskAsync(TodoItem, isNewItem);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task Delete()
    {
        await _todoService.DeleteTaskAsync(TodoItem);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
}
