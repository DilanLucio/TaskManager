using System.ComponentModel.DataAnnotations;
using TaskManager.Data.Enums;
using TaskManager.Models.ModelsValidations;
using TaskStatus = TaskManager.Data.Enums.TaskStatus;

namespace TaskManager.Models
{
    public class NewTaskModel
    {
        [Required(ErrorMessage = "The Title is required.")]
        [StringLength(100, ErrorMessage = "The title must not exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Description is required.")]
        [StringLength(500, ErrorMessage = "The description must not exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The StartDate is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid StartDate.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The EndDate is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid EndDate.")]
        [NewTaskModelValidations("StartDate", ErrorMessage = "The EndDate must be later than the start date.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "The Status is required.")]
        public TaskStatus Status { get; set; }

        [Required(ErrorMessage = "The Priority is required.")]
        public TaskPriorities Priority { get; set; }
    }
}