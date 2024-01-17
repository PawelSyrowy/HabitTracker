using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace XamarinApp.ViewModels
{
    [QueryProperty(nameof(TaskId), nameof(TaskId))]
    public class TaskDetailViewModel : BaseViewModel
    {
        public TaskDetailViewModel()
        {
            DeleteTaskCommand = new Command(OnDeleteTask);
        }

        private int taskId;
        public int TaskId
        {
            get
            {
                return taskId;
            }
            set
            {
                taskId = value;
                LoadTaskId(value);
            }
        }
        public async void LoadTaskId(int taskId)
        {
            try
            {
                var task = await DataStoreTasks.GetItemAsync(taskId);
                Id = task.Id;
                IsPriority = task.IsPriority;
                IsCompleted= task.IsCompleted;
                Priority = task.IsPriority ? "Priority" : "Regular";
                Completed = task.IsCompleted ? "Completed" : "Pending";
                CompleteUntil = task.CompleteUntil;
                Text = task.Text;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private string text;
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        private string priority;
        public string Priority
        {
            get => priority;
            set => SetProperty(ref priority, value);
        }
        private bool isPriority;
        public bool IsPriority
        {
            get => isPriority;
            set => SetProperty(ref isPriority, value);
        }
        
        private string completed;
        public string Completed
        {
            get => completed;
            set => SetProperty(ref completed, value);
        }
        private bool isCompleted;
        public bool IsCompleted
        {
            get => isCompleted;
            set => SetProperty(ref isCompleted, value);
        }

        private DateTime completeUntil;
        public DateTime CompleteUntil
        {
            get => completeUntil;
            set => SetProperty(ref completeUntil, value);
        }

        public int Id { get; set; }
        
        public async void OnToggleIsCompleted(object sender, ToggledEventArgs e)
        {
            try
            {
                var task = await DataStoreTasks.GetItemAsync(taskId);
                task.IsCompleted = e.Value;
                IsCompleted = task.IsCompleted;
                if (task.IsCompleted)
                {
                    Completed = "Completed";
                    task.CompletedOn = DateTime.Now.Date;
                }
                else
                {
                    Completed = "Pending";
                    task.CompletedOn = null;
                }
                await DataStoreTasks.UpdateItemAsync(task);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Toggle IsCompleted");
            }
        }

        public async void OnToggleIsPriority(object sender, ToggledEventArgs e)
        {
            try
            {
                var task = await DataStoreTasks.GetItemAsync(taskId);
                task.IsPriority = e.Value;
                IsPriority = task.IsPriority;
                Priority = task.IsPriority ? "Priority" : "Regular";
                await DataStoreTasks.UpdateItemAsync(task);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Toggle IsPriority");
            }
        }

        public Command DeleteTaskCommand { get; }
        private async void OnDeleteTask()
        {
            try
            {
                await DataStoreTasks.DeleteItemAsync(taskId);

                await Shell.Current.GoToAsync("..");

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Delete Task");
            }
        }
        
        public async void OnTextChanged(string newText)
        {
            try
            {
                var task = await DataStoreTasks.GetItemAsync(taskId);
                if (!String.IsNullOrWhiteSpace(Text))
                {
                    task.Text = Text;
                    await DataStoreTasks.UpdateItemAsync(task);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to change Title");
            }
        }

        public async void OnCompleteUntilChanged(DateTime newDate)
        {
            try
            {
                var task = await DataStoreTasks.GetItemAsync(taskId);
                task.CompleteUntil = CompleteUntil;
                await DataStoreTasks.UpdateItemAsync(task);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to change Deadline");
            }
        }
    }
}
