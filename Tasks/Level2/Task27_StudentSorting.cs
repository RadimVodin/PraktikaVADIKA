using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(27, 2)]
public sealed class Task27_StudentSorting : ITask
{
    public string Title => "IComparable / IComparer<T> для Student";

    public string Description =>
        "Научить Student сортироваться.";

    public string[] Requirements => new[]
    {
        "Класс Student: Name, AverageGrade",
        "Реализовать IComparable<Student> — сортировка по среднему баллу (по убыванию)",
        "Реализовать IComparer<Student> ByNameComparer — сортировка по имени",
        "Продемонстрировать обе сортировки на List<Student>"
    };

    public void Run()
    {
        List<Student> students = new List<Student>
        {
            new Student("Иван", 4.5),
            new Student("Анна", 4.9),
            new Student("Петр", 3.8),
            new Student("Мария", 5.0),
            new Student("Алексей", 4.2)
        };

        Console.WriteLine("Исходный список студентов:");
        PrintList(students);

        // Сортировка 1: По среднему баллу (по убыванию), через IComparable
        students.Sort(); // Вызывает CompareTo у Student
        Console.WriteLine("\nСортировка по среднему баллу (по убыванию):");
        PrintList(students);

        // Сортировка 2: По имени, через IComparer
        students.Sort(new ByNameComparer());
        Console.WriteLine("\nСортировка по имени (А-Я):");
        PrintList(students);
    }

    private void PrintList(List<Student> list)
    {
        foreach (var s in list)
        {
            Console.WriteLine($"  {s.Name,-10} | Балл: {s.AverageGrade:F2}");
        }
    }
}

// Класс Студента
public class Student : IComparable<Student>
{
    public string Name { get; set; }
    public double AverageGrade { get; set; }

    public Student(string name, double averageGrade)
    {
        Name = name;
        AverageGrade = averageGrade;
    }

    // Реализация IComparable<Student>
    // Сортируем по убыванию балла (если s2 больше, то s1 идет позже)
    public int CompareTo(Student? other)
    {
        if (other == null) return 1;
        // Возвращаем результат сравнения "наоборот" для сортировки по убыванию
        return other.AverageGrade.CompareTo(this.AverageGrade);
    }
}

// Реализация IComparer<Student> для сортировки по имени
public class ByNameComparer : IComparer<Student>
{
    public int Compare(Student? x, Student? y)
    {
        if (x == null || y == null) return 0;
        // Сравниваем строки по алфавиту
        return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
    }
}