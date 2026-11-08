using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(28, 2)]
public sealed class Task28_PointEquals : ITask
{
    public string Title => "Equals / GetHashCode для Point";

    public string Description =>
        "Переопределить Equals и GetHashCode для класса Point(x, y).";

    public string[] Requirements => new[]
    {
        "Переопределить Equals(object) и GetHashCode()",
        "Реализовать IEquatable<Point>",
        "Проверить: две точки с одинаковыми координатами равны",
        "Проверить в HashSet<Point>: добавление дубликата не увеличивает Count"
    };

    public void Run()
    {
        Point p1 = new Point(5, 10);
        Point p2 = new Point(5, 10); // Дубликат
        Point p3 = new Point(7, 8);

        Console.WriteLine($"p1: {p1}");
        Console.WriteLine($"p2: {p2}");
        Console.WriteLine($"p3: {p3}");
        Console.WriteLine("--------------------------------------------------");

        // Проверка Equals
        Console.WriteLine($"p1.Equals(p2): {p1.Equals(p2)}"); // true
        Console.WriteLine($"p1.Equals(p3): {p1.Equals(p3)}"); // false
        Console.WriteLine($"p1 == p2: {p1 == p2}"); // true (благодаря перегрузке оператора)

        // Проверка в HashSet
        HashSet<Point> points = new HashSet<Point>();
        points.Add(p1);
        points.Add(p2); // Должен проигнорироваться, так как p1 и p2 равны
        points.Add(p3);

        Console.WriteLine($"\nКоличество уникальных точек в HashSet: {points.Count}");
        Console.WriteLine("Ожидалось: 2 (так как p1 и p2 одинаковые)");
    }
}

public class Point : IEquatable<Point>
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Реализация IEquatable<Point>
    public bool Equals(Point? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return X == other.X && Y == other.Y;
    }

    // Переопределение Equals(object)
    public override bool Equals(object? obj)
    {
        return Equals(obj as Point);
    }

    // Переопределение GetHashCode
    public override int GetHashCode()
    {
        // Комбинируем хеш-коды X и Y
        return HashCode.Combine(X, Y);
    }

    // Перегрузка операторов == и != (хороший тон при реализации Equals)
    public static bool operator ==(Point? left, Point? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Point? left, Point? right)
    {
        return !Equals(left, right);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}