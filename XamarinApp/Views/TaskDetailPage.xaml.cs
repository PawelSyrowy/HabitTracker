using Xamarin.Forms;
using XamarinApp.ViewModels;

namespace XamarinApp.Views
{
    public partial class TaskDetailPage : ContentPage
    {
        public TaskDetailPage()
        {
            InitializeComponent();
            BindingContext = new TaskDetailViewModel();
        }
        private void OnToggleIsPriority(object sender, ToggledEventArgs e)
        {
            if (BindingContext is TaskDetailViewModel viewModel)
            {
                viewModel.OnToggleIsPriority(sender, e);
            }
        }
        private void OnToggleIsCompleted(object sender, ToggledEventArgs e)
        {
            if (BindingContext is TaskDetailViewModel viewModel)
            {
                viewModel.OnToggleIsCompleted(sender, e);
            }
        }
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (BindingContext is TaskDetailViewModel viewModel)
            {
                viewModel.OnTextChanged(e.NewTextValue);
            }
        }
        private void OnCompleteUntilChanged(object sender, DateChangedEventArgs  e)
        {
            if (BindingContext is TaskDetailViewModel viewModel)
            {
                viewModel.OnCompleteUntilChanged(e.NewDate);
            }
        }
        private void OnNotesChanged(object sennder, TextChangedEventArgs e)
        {
            if (BindingContext is TaskDetailViewModel viewModel)
            {
                viewModel.OnNotesChanged(e.NewTextValue);
            }
        }
    }
}