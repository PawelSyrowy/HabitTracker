using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinApp.Models;
using XamarinApp.Views;

namespace XamarinApp.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        public TasksViewModel()
        {
            Title = "Tasks";
            Tasks = new ObservableCollection<TaskModel>();
            LoadTasksCommand = new Command(async () => await ExecuteLoadTasksCommand());
            AddTaskCommand = new Command(OnAddTask);
            TaskTapped = new Command<TaskModel>(OnTaskTapped);
            DeleteCommand = new Command<TaskModel>(OnTaskDelete);
            IsCompletedCommand = new Command<TaskModel>(OnToggleIsCompleted);

        }
        private TaskModel _selectedTask; 
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedTask = null;
        }
        public TaskModel SelectedTask
        {
            get => _selectedTask;
            set
            {
                SetProperty(ref _selectedTask, value);
                OnTaskTapped(value);
            }
        }

        public ObservableCollection<TaskModel> Tasks { get; }
        public Command LoadTasksCommand { get; }
        public async Task ExecuteLoadTasksCommand()
        {
            IsBusy = true;

            try
            {
                var items = await DataStoreTasks.GetItemsAsync(true);
                Tasks.Clear();

                foreach (var item in items.OrderBy(task => task.CreatedOn))
                {
                    Tasks.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Command AddTaskCommand { get; }
        private async void OnAddTask(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewTaskPage));
        }

        public Command<TaskModel> TaskTapped { get; }
        async void OnTaskTapped(TaskModel task)
        {
            if (task == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TaskDetailPage)}?{nameof(TaskDetailViewModel.TaskId)}={task.Id}");
        }
        public Command<TaskModel> DeleteCommand { get; }
        private async void OnTaskDelete(TaskModel task)
        {
            if (task == null)
                return;

            try
            {
                await DataStoreTasks.DeleteItemAsync(task.Id);

                Tasks.Clear();
                await ExecuteLoadTasksCommand();

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Delete Task");
            }
        }
        public Command<TaskModel> IsCompletedCommand { get; }
        private async void OnToggleIsCompleted(TaskModel task)
        {
            if (task == null)
                return;

            try
            {
                await DataStoreTasks.UpdateItemAsync(task);
                Tasks.Clear();
                await ExecuteLoadTasksCommand();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to toggle completion status: {ex.Message}");
            }
        }


    }
}