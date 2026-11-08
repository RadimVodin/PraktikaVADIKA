using CSharpPracticum.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(33, 2)]
public sealed class Task33_LinqEmployees : ITask
{
    public string Title => "LINQ: сотрудники (top-3, avg по отделу, фильтр)";

    public string Description =>
        "LINQ-запросы по списку сотрудников.";

    public string[] Requirements => new[]
    {
        "Класс Employee: Name, Age, Salary, Department",
        "Список из 10+ сотрудников",
        "Запросы через LINQ: топ-3 по зарплате (убывание), средняя зарплата по каждому отделу (GroupBy + Average), сотрудники старше 30 (Where)",
        "Вывести результаты"
    };

    public void Run()
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee("Иван", 25, 50000m, "IT"),
            new Employee("Анна", 32, 80000m, "IT"),
            new Employee("Петр", 45, 60000m, "HR"),
            new Employee("Мария", 28, 55000m, "HR"),
            new Employee("Алексей", 35, 90000m, "IT"),
            new Employee("Ольга", 41, 75000m, "Finance"),
            new Employee("Дмитрий", 29, 48000m, "Finance"),
            new Employee("Елена", 38, 85000m, "IT"),
            new Employee("Сергей", 50, 110000m, "Management"),
            new Employee("Татьяна", 26, 52000m, "Management"),
            new Employee("Андрей", 31, 65000m, "HR"),
            new Employee("Юлия", 24, 45000m, "Finance")
        };

        // 1. Топ-3 по зарплате (убывание)
        var top3 = employees
            .OrderByDescending(e => e.Salary)
            .Take(3)
            .ToList();

        Console.WriteLine("=== ТОП-3 СОТРУДНИКА ПО ЗАРПЛАТЕ ===");
        foreach (var e in top3)
        {
            Console.WriteLine($"{e.Name,-10} | Зарплата: {e.Salary:C} | Отдел: {e.Department}");
        }
        Console.WriteLine();

        // 2. Средняя зарплата по каждому отделу
        var avgByDept = employees
            .GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                AverageSalary = g.Average(e => e.Salary)
            })
            .OrderByDescending(x => x.AverageSalary)
            .ToList();

        Console.WriteLine("=== СРЕДНЯЯ ЗАРПЛАТА ПО ОТДЕЛАМ ===");
        foreach (var dept in avgByDept)
        {
            Console.WriteLine($"{dept.Department,-12} | Средняя зарплата: {dept.AverageSalary:C}");
        }
        Console.WriteLine();

        // 3. Сотрудники старше 30
        var olderThan30 = employees
            .Where(e => e.Age > 30)
            .OrderBy(e => e.Age)
            .ToList();

        Console.WriteLine("=== СОТРУДНИКИ СТАРШЕ 30 ЛЕТ ===");
        foreach (var e in olderThan30)
        {
            Console.WriteLine($"{e.Name,-10} | Возраст: {e.Age} | Отдел: {e.Department}");
        }
    }
}

// Класс сотрудника
public class Employee
{
    public string Name { get; set; }
    public int Age { get; set; }
    public decimal Salary { get; set; }
    public string Department { get; set; }

    public Employee(string name, int age, decimal salary, string department)
    {
        Name = name;
        Age = age;
        Salary = salary;
        Department = department;
    }
}