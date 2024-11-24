using Task = TaskManager.Data.Entities.Task;

namespace TaskManager.Models
{
    public class TasksViewModel
    {
        public List<Task> Tasks { get; set; }
        public List<Task> TasksCompleted { get; set; }
        public NewTaskModel NewTask { get; set; }
        public Task TaskSelected { get; set; }
    }
}
