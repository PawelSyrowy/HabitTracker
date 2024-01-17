using System;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Services;
using XamarinApp.ViewModels;

namespace XamarinApp.Views
{
    public partial class TasksPage : ContentPage
    {
        TasksViewModel _viewModel;

        public TasksPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new TasksViewModel(); 
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
