using MauiTodoApp.Views;

namespace MauiTodoApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(TodoItemPage), typeof(TodoItemPage));
    }
}
