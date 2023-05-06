using MauiTodoApp.ViewModels;

namespace MauiTodoApp.Views
{
    public partial class TodoListPage : ContentPage
    {
        TodoListViewModel vm;

        public TodoListPage(TodoListViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            this.vm = vm;         
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.GetItemsCommand.Execute(null);
        }
    }
}
