using System;
using Xamarin.Forms;
using XamarinApp.Models;

namespace XamarinApp.ViewModels
{
    public class NewTaskViewModel : BaseViewModel
    {
        public NewTaskViewModel()
        {
            CompleteUntil = DateTime.Now;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }
        
        private string text;
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                SetProperty(ref isChecked, value);
            }
        }

        private DateTime completeUntil;
        public DateTime CompleteUntil
        {
            get => completeUntil;
            set => SetProperty(ref completeUntil, value);
        }

        public Command SaveCommand { get; }
        private async void OnSave() //currently not used 
        {
            TaskModel newItem = new TaskModel(Guid.NewGuid().GetHashCode(), Text, CompleteUntil, isChecked);
            await DataStoreTasks.AddItemAsync(newItem);
            await Shell.Current.GoToAsync("..");
        }
        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text);
        }

        public Command CancelCommand { get; }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
