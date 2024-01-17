using SQLite;
using System;

namespace XamarinApp.Models
{
    public class TaskModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime CompleteUntil { get; set; }
        public DateTime ? CompletedOn { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsPriority { get; set; }

        public TaskModel(int id, string text, DateTime completeUntil, bool isPriority = false)
        {
            Id = id;
            Text = text;
            CreatedOn = DateTime.Now;
            CompleteUntil = completeUntil;
            CompletedOn = null;
            IsCompleted = false;
            IsPriority = isPriority;
        }
        public TaskModel()
        {
            // Empty constructor
        }
    }
}
