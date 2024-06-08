using Microsoft.EntityFrameworkCore;
using ConsoleApp1.Model;
using Task = ConsoleApp1.Model.Task;


namespace ConsoleApp1;

public class Program
{
    static DateOnly ParseTerm(string input)
    {
        DateOnly term;
        while (!DateOnly.TryParse(input, out term))
        {
            Console.WriteLine("Неправильный ввод. Попробуйте еще раз");
            Console.Write("Введите дату выполнения задачи (в формате ГГГГ-ММ-ДД): ");
            input = Console.ReadLine();
        }
        return term;
    }
    
    public static void Main(string[] args)
    {
        NotebookContext db = new NotebookContext();
        
         bool isExists = false;
            Model.User user = new User();
            

            while (!isExists)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Выберите действие: ");
                Console.WriteLine("1. Зарегистрироваться");
                Console.WriteLine("2. Войти в аккаунт");
                Console.WriteLine("--------------------------------");
                try
                {
                    int number1 = int.Parse(Console.ReadLine());
                    switch (number1)
                    {
                        case 1:
                            Console.WriteLine("Введите id пользователя: ");
                            user.IdUser = int.Parse(Console.ReadLine());
                            
                            Console.WriteLine("Введите логин: ");
                            user.Username = Console.ReadLine();

                            Console.WriteLine("Введите пароль: ");
                            user.Password = Console.ReadLine();
                            WorkProgram.SaveUser(user, db);
                            isExists = true;
                            break;
                        case 2:
                            Console.WriteLine("Введите id пользователя: ");
                            user.IdUser = int.Parse(Console.ReadLine());
                            
                            Console.WriteLine("Введите логин: ");
                            user.Username = Console.ReadLine();

                            Console.WriteLine("Введите пароль: ");
                            user.Password = Console.ReadLine();

                            isExists = WorkProgram.CheckUser(user, db);
                            break;
                    }
                    if (isExists)
                    {
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine("Вы успешно авторизовались!");
                    }
                    else
                    {
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine("Неверный ввод!");
                        Console.WriteLine("--------------------------------");
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Неккоректный ввод!");
                }
            }

            while (true)
                {
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Выберите действие: ");
                    Console.WriteLine("1. Просмотреть все задачи");
                    Console.WriteLine("2. Просмотреть задачи на сегодня");
                    Console.WriteLine("3. Просмотреть задачи на завтра");
                    Console.WriteLine("4. Просмотреть задачи на неделю");
                    Console.WriteLine("5. Просмотреть предстоящие задачи");
                    Console.WriteLine("6. Просмотреть прошедшие задачи");
                    Console.WriteLine("7. Добавить новую задачу");
                    Console.WriteLine("8. Редактировать задачу");
                    Console.WriteLine("9. Удалить задачу");
                    Console.WriteLine("0. Выйти из программы");
                    Console.WriteLine("--------------------------------");

                    try
                    { 
                        Console.WriteLine("Ваш выбор: ");
                    int number = int.Parse(Console.ReadLine());

                    switch (number)
                    {
                        case 1:
                            Console.WriteLine("Все задачи: ");
                            WorkProgram.ViewAllTodos(db, user);
                            break;
                        case 2:
                            Console.WriteLine("Задачи на сегодня: ");
                            WorkProgram.ViewTodosByDate(db, user, DateOnly.FromDateTime(DateTime.Today));
                            break;
                        case 3:
                            Console.WriteLine("Задачи на завтра: ");
                            WorkProgram.ViewTodosByDate(db, user,  DateOnly.FromDateTime(DateTime.Today.AddDays(1)));
                            break;
                        case 4:
                            Console.WriteLine("Задачи через неделю: ");
                            WorkProgram.ViewTodosByDate(db, user,  DateOnly.FromDateTime(DateTime.Today.AddDays(7)));
                            break;
                        case 5:
                            Console.WriteLine("Предстоящие задачи : ");
                            WorkProgram.ViewUpcomingTasks(db, user, DateOnly.FromDateTime(DateTime.Today));
                            break;
                        case 6:
                            Console.WriteLine("Прошедшие задачи: ");
                            WorkProgram.ViewCompletedTasks(db, user, DateOnly.FromDateTime(DateTime.Today));
                            break;
                        case 7:
                            Model.Task task = new Task();
                            Console.WriteLine("Введите название задачи: ");
                            task.Title = Console.ReadLine();

                            Console.WriteLine("Введите описание задачи: ");
                            task.Description = Console.ReadLine();

                            Console.WriteLine("Введите дату выполнения задачи (в формате ГГГГ-ММ-ДД): ");
                            task.Term = ParseTerm(Console.ReadLine());

                            task.IdUser = user.IdUser;
                            
                            WorkProgram.AddTask(task, db);
                            Console.WriteLine("Задача успешно добавлена!");
                            break;
                       case 8:
                            Model.Task taskEdit = new Task();
                            Console.WriteLine("Введите название задачи, которую хотите изменить: ");
                            string tempTitle = Console.ReadLine();
                            
                            Console.WriteLine("Введите название задачи: ");
                            taskEdit.Title = Console.ReadLine();

                            Console.WriteLine("Введите описание задачи: ");
                            taskEdit.Description = Console.ReadLine();

                            Console.WriteLine("Введите дату выполнения задачи (в формате ГГГГ-ММ-ДД): ");
                            taskEdit.Term = ParseTerm(Console.ReadLine());
                            
                            WorkProgram.EditTask(taskEdit, user, db, tempTitle);
                            break;
                        case 9:
                            Console.WriteLine("Введите название задачи, которую хотите удалить: "); 
                            string taskTitle = Console.ReadLine();
                            WorkProgram.RemoveTask(taskTitle,user, db);
                            break;
                        case 0:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Неправильный ввод. Попробуйте еще раз");
                            break;
                    }
                    }
                    
                    catch (FormatException )
                    {
                        Console.WriteLine("Неккоректный ввод!");
                    }
                }
    }
}