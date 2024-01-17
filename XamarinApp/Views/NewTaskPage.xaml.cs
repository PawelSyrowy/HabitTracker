using System;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Services;
using XamarinApp.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace XamarinApp.Views
{
    public partial class NewTaskPage : ContentPage
    {
        public NewTaskViewModel ViewModel { get; set; }

        public NewTaskPage()
        {
            InitializeComponent();
            ViewModel = new NewTaskViewModel();
            BindingContext = ViewModel;
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = new TaskModel(Guid.NewGuid().GetHashCode(), ViewModel.Text, ViewModel.CompleteUntil, ViewModel.IsChecked);
            DataStoreTasks database = await DataStoreTasks.Instance;
            await database.SaveItemAsync(todoItem);
            //await Navigation.PopAsync();
            await Shell.Current.GoToAsync("..");
        }
    }
}