using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CSharpPracticum.Tasks.Level3;

[TaskInfo(44, 3)]
public sealed class Task44_TodoList : ITask
{
    public string Title => "Мини-проект «Todo List» (классы + JSON)";

    public string Description =>
        "Консольное приложение для списка задач с сохранением в JSON.";

    public string[] Requirements => new[]
    {
        "Классы: TaskItem (Id, Title, IsDone), TaskRepository (CRUD), TaskService (бизнес-логика)",
        "CRUD-операции: добавить, вывести все, отметить выполненной, удалить",
        "Сохранение/загрузка через System.Text.Json (файл tasks.json)",
        "Меню с выбором действий",
        "Обработка ошибок (некорректный ввод, отсутствующий файл)"
    };

    public void Run()
    {
        // Создаем репозиторий (он сам загрузит данные из файла, если он есть)
        var repository = new TaskRepository("tasks.json");
        var service = new TaskService(repository);

        while (true)
        {
            Console.WriteLine("\n=== TODO LIST ===");
            Console.WriteLine("1. Показать все задачи");
            Console.WriteLine("2. Добавить задачу");
            Console.WriteLine("3. Отметить задачу выполненной");
            Console.WriteLine("4. Удалить задачу");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");

            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    service.PrintAllTasks();
                    break;
                case "2":
                    Console.Write("Введите название задачи: ");
                    string? title = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(title))
                    {
                        service.AddTask(title);
                        Console.WriteLine("Задача добавлена.");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: название не может быть пустым.");
                    }
                    break;
                case "3":
                    Console.Write("Введите ID задачи для отметки: ");
                    if (int.TryParse(Console.ReadLine(), out int idDone))
                    {
                        if (service.MarkAsDone(idDone))
                            Console.WriteLine("Задача отмечена выполненной.");
                        else
                            Console.WriteLine("Задача с таким ID не найдена.");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: ID должен быть числом.");
                    }
                    break;
                case "4":
                    Console.Write("Введите ID задачи для удаления: ");
                    if (int.TryParse(Console.ReadLine(), out int idDel))
                    {
                        if (service.DeleteTask(idDel))
                            Console.WriteLine("Задача удалена.");
                        else
                            Console.WriteLine("Задача с таким ID не найдена.");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: ID должен быть числом.");
                    }
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    break;
            }
        }
    }
}

// --- Модель данных ---
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public bool IsDone { get; set; }
}

// --- Репозиторий (работа с файлом) ---
public class TaskRepository
{
    private readonly string _filePath;
    private List<TaskItem> _tasks;

    public TaskRepository(string filePath)
    {
        _filePath = filePath;
        _tasks = LoadFromFile();
    }

    public List<TaskItem> GetAll() => _tasks;

    public void Add(TaskItem task)
    {
        _tasks.Add(task);
        SaveToFile();
    }

    public void Update()
    {
        SaveToFile();
    }

    public void Delete(TaskItem task)
    {
        _tasks.Remove(task);
        SaveToFile();
    }

    private List<TaskItem> LoadFromFile()
    {
        if (!File.Exists(_filePath)) return new List<TaskItem>();

        try
        {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }
        catch
        {
            // Если файл поврежден, возвращаем пустой список
            return new List<TaskItem>();
        }
    }

    private void SaveToFile()
    {
        try
        {
            string json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сохранения: {ex.Message}");
        }
    }
}

// --- Сервис (бизнес-логика) ---
public class TaskService
{
    private readonly TaskRepository _repository;

    public TaskService(TaskRepository repository)
    {
        _repository = repository;
    }

    public void PrintAllTasks()
    {
        var tasks = _repository.GetAll();
        if (tasks.Count == 0)
        {
            Console.WriteLine("Список задач пуст.");
            return;
        }

        Console.WriteLine("\n--- Список задач ---");
        foreach (var task in tasks)
        {
            string status = task.IsDone ? "[V]" : "[ ]";
            Console.WriteLine($"{status} ID: {task.Id} | {task.Title}");
        }
    }

    public void AddTask(string title)
    {
        var tasks = _repository.GetAll();
        int newId = tasks.Count > 0 ? tasks[^1].Id + 1 : 1; // Генерируем новый ID

        var newTask = new TaskItem { Id = newId, Title = title, IsDone = false };
        _repository.Add(newTask);
    }

    public bool MarkAsDone(int id)
    {
        var task = _repository.GetAll().Find(t => t.Id == id);
        if (task == null) return false;

        task.IsDone = true;
        _repository.Update();
        return true;
    }

    public bool DeleteTask(int id)
    {
        var task = _repository.GetAll().Find(t => t.Id == id);
        if (task == null) return false;

        _repository.Delete(task);
        return true;
    }
}