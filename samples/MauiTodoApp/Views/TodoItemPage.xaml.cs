using MauiTodoApp.ViewModels;

namespace MauiTodoApp.Views
{
    public partial class TodoItemPage : ContentPage
    {
        public TodoItemPage(TodoItemViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }
    }
}
