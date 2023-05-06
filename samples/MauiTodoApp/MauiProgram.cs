using Microsoft.Extensions.Logging;
using GenericHttpClientRepository;
using MauiTodoApp.Services;
using MauiTodoApp.ViewModels;
using MauiTodoApp.Views;

namespace MauiTodoApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddGenericHttpClientRepository();

        builder.Services.AddSingleton(Connectivity.Current);
        builder.Services.AddSingleton<ITodoService, TodoService>();

        builder.Services.AddSingleton<TodoListViewModel>();
        builder.Services.AddSingleton<TodoListPage>();

        builder.Services.AddTransient<TodoItemViewModel>();
        builder.Services.AddTransient<TodoItemPage>();

        return builder.Build();
	}
}
