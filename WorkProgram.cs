namespace ConsoleApp1;
using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Model;

public class WorkProgram
{
    public static void SaveUser(Model.User user,  NotebookContext db)
    {
        db.Users.Add(user);
        db.SaveChanges();
    }
    
    public static bool CheckUser(Model.User user,  NotebookContext db)
    {
        var testUser = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

        if (testUser != null)
        {
            return true;
        }
        return false;
    }

    public static void ViewAllTodos(NotebookContext db, Model.User user)
    {
        var todos = db.Tasks.Include(t => t.IdUserNavigation).Where(t => t.IdUser == user.IdUser ).ToList();;

        foreach (var todo in todos)
        {
            Console.WriteLine($"Название: {todo.Title}");
            Console.WriteLine($"Описание: {todo.Description}");
            Console.WriteLine($"Дата: {todo.Term.ToShortDateString()}");
            Console.WriteLine();
        }
    }
    
    public static void ViewTodosByDate(NotebookContext db, Model.User user, DateOnly staticDate)
    {
        var todos = db.Tasks.Include(t => t.IdUserNavigation).Where(t => t.IdUser == user.IdUser && t.Term == staticDate).ToList();

        foreach (var todo in todos)
        {
            Console.WriteLine($"Название: {todo.Title}");
            Console.WriteLine($"Описание: {todo.Description}");
            Console.WriteLine($"Дата: {todo.Term.ToShortDateString()}");
            Console.WriteLine();
        }
    }
    
    public static void ViewUpcomingTasks(NotebookContext db, Model.User user, DateOnly staticDate)
    {
        var todos = db.Tasks.Include(t => t.IdUserNavigation).Where(t => t.IdUser == user.IdUser && t.Term > staticDate).ToList();

        foreach (var todo in todos)
        {
            Console.WriteLine($"Название: {todo.Title}");
            Console.WriteLine($"Описание: {todo.Description}");
            Console.WriteLine($"Дата: {todo.Term.ToShortDateString()}");
            Console.WriteLine();
        }
    }
    
    public static void ViewCompletedTasks(NotebookContext db, Model.User user, DateOnly staticDate)
    {
        var todos = db.Tasks.Include(t => t.IdUserNavigation).Where(t => t.IdUser == user.IdUser && t.Term < staticDate).ToList();

        foreach (var todo in todos)
        {
            Console.WriteLine($"Название: {todo.Title}");
            Console.WriteLine($"Описание: {todo.Description}");
            Console.WriteLine($"Дата: {todo.Term.ToShortDateString()}");
            Console.WriteLine();
        }
    }
    
    public static void RemoveTask(string taskTitle, Model.User user, NotebookContext db)
    {
        var taskToDelete = db.Tasks.FirstOrDefault(t => t.Title == taskTitle && t.IdUser == user.IdUser);
        if(taskToDelete != null)
        {
            db.Tasks.Remove(taskToDelete);
            db.SaveChanges();
            Console.WriteLine("Задача успешно удалена.");
        }
        else
        {
            Console.WriteLine($"Задача с названием {taskTitle} не найдена.");
        }

    }
    public static void AddTask(Model.Task task, NotebookContext db)
    {
        db.Tasks.Add(task);
        db.SaveChanges();
    }
    
    public static void EditTask(Model.Task task, Model.User user, NotebookContext db, string taskTitle)
    {
        var taskToEdit = db.Tasks.FirstOrDefault(t => t.Title == taskTitle && t.IdUser == user.IdUser);
        if(taskToEdit != null)
        {
            taskToEdit.Title = task.Title;
            taskToEdit.Term = task.Term;
            taskToEdit.Description = task.Description;
            db.SaveChanges();
            Console.WriteLine("Задача успешно обновлена!");
        }
        else
        {
            Console.WriteLine($"Задача с названием {taskTitle} не найдена");
        }
    }
}