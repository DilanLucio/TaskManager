using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Data.Enums;
using TaskManager.Models;
using TaskStatus = TaskManager.Data.Enums.TaskStatus;

namespace TaskManager.Controllers
{
    public class TasksController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext DBContext = context;

        public async Task<IActionResult> Index()
        {
            ViewBag.TaskStatusList = Enum.GetValues(typeof(TaskStatus))
                               .Cast<TaskStatus>()
                               .Select(s => new SelectListItem
                               {
                                   Value = s.ToString(),
                                   Text = s.ToString()
                               })
                               .ToList();

            ViewBag.TaskPrioritiesList = Enum.GetValues(typeof(TaskPriorities))
                               .Cast<TaskPriorities>()
                               .Select(s => new SelectListItem
                               {
                                   Value = s.ToString(),
                                   Text = s.ToString()
                               })
                               .ToList();

            TasksViewModel model = new()
            {
                Tasks = await DBContext.Tasks.ToListAsync(),
                TasksCompleted = await DBContext.Tasks.Where(t => t.Status == (int)TaskStatus.Completed).ToListAsync(),
                NewTask = new() { StartDate = DateTime.Now, EndDate = DateTime.Now }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Create(NewTaskModel newTask)
        {
            if (ModelState.IsValid)
            {
                Data.Entities.Task task = new()
                {
                    Title = newTask.Title,
                    Description = newTask.Description,
                    StartDate = newTask.StartDate,
                    EndDate = newTask.EndDate,
                    Status = (int)newTask.Status,
                    Priority = (int)newTask.Priority
                };
                DBContext.Add(task);
                await DBContext.SaveChangesAsync();
                return Json(new { success = true, message = "Tarea creada correctamente" });
            }
            else
            {
                List<string> errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

                return Json(new { success = false, message = "Datos inválidos", errors });
            }
        }

        [HttpPost]
        public async Task<JsonResult> SelectTask(int Id)
        {
            Data.Entities.Task task = await DBContext.Tasks.Where(t => t.Id == Id).FirstOrDefaultAsync();
            if (task != null)
            {
                return Json(new { success = true, taskSelected = task });
            }
            else
            {
                return Json(new { success = false, message = "Task not found", errors = "" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTask(Data.Entities.Task task)
        {
            var existingTask = await DBContext.Tasks
                                      .Where(t => t.Id == task.Id)
                                      .FirstOrDefaultAsync();

            if (existingTask != null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.StartDate = task.StartDate;
                existingTask.EndDate = task.EndDate;
                existingTask.Status = task.Status;
                existingTask.Priority = task.Priority;

                await DBContext.SaveChangesAsync();
                return Json(new { success = true, message = "Task updated" });
            }
            else
            {
                return Json(new { success = false, message = "Task not found", errors = "" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> CompleteTask(int Id)
        {
            Data.Entities.Task task = await DBContext.Tasks.Where(t => t.Id == Id).FirstOrDefaultAsync();
            if (task != null)
            {
                task.Status = (int)TaskStatus.Completed;
                await DBContext.SaveChangesAsync();
                return Json(new { success = true, message = "Task completed" });
            }
            else
            {
                return Json(new { success = false, message = "Task not found", errors = "" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTask(int Id)
        {
            Data.Entities.Task task = await DBContext.Tasks.Where(t => t.Id == Id).FirstOrDefaultAsync();
            if (task != null)
            {
                DBContext.Remove(task);
                await DBContext.SaveChangesAsync();
                return Json(new { success = true, message = "Task deleted" });
            }
            else
            {
                return Json(new { success = false, message = "Task not found", errors = "" });
            }
        }
    }
}
