using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(25, 2)]
public sealed class Task25_Shapes : ITask
{
    public string Title => "Интерфейс IShape + сортировка фигур";

    public string Description =>
        "Реализовать интерфейс фигур и отсортировать их.";

    public string[] Requirements => new[]
    {
        "IShape с методами Area() и Perimeter()",
        "Классы Circle, Rectangle, Triangle — реализуют IShape",
        "Создать List<IShape> из 5+ фигур",
        "Отсортировать по площади (через List.Sort с лямбдой)",
        "Вывести фигуры с площадями до и после сортировки"
    };

    public void Run()
    {
        List<IShape> shapes = new List<IShape>
        {
            new Rectangle(10, 5),
            new Circle(7),
            new Triangle(3, 4, 5),
            new Circle(2),
            new Rectangle(3, 3)
        };

        Console.WriteLine("Фигуры ДО сортировки:");
        PrintShapes(shapes);

        // Сортировка по площади (по возрастанию) с помощью лямбда-выражения
        shapes.Sort((s1, s2) => s1.Area().CompareTo(s2.Area()));

        Console.WriteLine("\nФигуры ПОСЛЕ сортировки (по возрастанию площади):");
        PrintShapes(shapes);
    }

    private void PrintShapes(List<IShape> shapes)
    {
        foreach (var shape in shapes)
        {
            // ToString переопределен в классах, выводит название фигуры
            Console.WriteLine($"{shape,-15} | Площадь: {shape.Area(),6:F2} | Периметр: {shape.Perimeter(),6:F2}");
        }
    }
}

// Интерфейс
public interface IShape
{
    double Area();
    double Perimeter();
}

// Класс Круг
public class Circle : IShape
{
    public double Radius { get; set; }

    public Circle(double radius) { Radius = radius; }

    public double Area() => Math.PI * Radius * Radius;
    public double Perimeter() => 2 * Math.PI * Radius;

    public override string ToString() => $"Круг (R={Radius})";
}

// Класс Прямоугольник
public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public double Area() => Width * Height;
    public double Perimeter() => 2 * (Width + Height);

    public override string ToString() => $"Прямоуг. ({Width}x{Height})";
}

// Класс Треугольник (по трем сторонам, формула Герона)
public class Triangle : IShape
{
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }

    public Triangle(double a, double b, double c)
    {
        A = a; B = b; C = c;
    }

    public double Area()
    {
        // Формула Герона
        double p = Perimeter() / 2;
        return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
    }

    public double Perimeter() => A + B + C;

    public override string ToString() => $"Треугольник ({A},{B},{C})";
}