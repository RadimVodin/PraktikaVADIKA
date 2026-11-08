using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(24, 2)]
public sealed class Task24_AnimalHierarchy : ITask
{
    public string Title => "ООП: Animal → Dog/Cat/Bird";

    public string Description =>
        "Построить иерархию животных.";

    public string[] Requirements => new[]
    {
        "Абстрактный класс Animal: свойство Name, абстрактный метод MakeSound(), переопределённый ToString()",
        "Классы Dog, Cat, Bird — наследники, реализуют MakeSound()",
        "Bird дополнительно имеет метод Fly()",
        "Продемонстрировать полиморфизм: List<Animal> с разными животными, вызов MakeSound() в цикле"
    };

    public void Run()
    {
        List<Animal> animals = new List<Animal>
        {
            new Dog("Шарик"),
            new Cat("Мурка"),
            new Bird("Кеша"),
            new Dog("Бобик")
        };

        Console.WriteLine("Список животных и их звуки:");
        Console.WriteLine("--------------------------------------------------");

        foreach (Animal animal in animals)
        {
            // Полиморфизм: вызывается переопределенный метод конкретного класса
            Console.WriteLine($"{animal} говорит: {animal.MakeSound()}");

            // Проверяем, является ли животное птицей, чтобы вызвать Fly()
            if (animal is Bird bird)
            {
                Console.WriteLine($"  -> {bird.Name} умеет летать: {bird.Fly()}");
            }
        }
    }
}

// Абстрактный базовый класс
public abstract class Animal
{
    public string Name { get; set; }

    public Animal(string name)
    {
        Name = name;
    }

    // Абстрактный метод (не имеет реализации в базовом классе)
    public abstract string MakeSound();

    // Переопределенный ToString()
    public override string ToString()
    {
        return $"[{GetType().Name}] {Name}";
    }
}

// Наследники
public class Dog : Animal
{
    public Dog(string name) : base(name) { }

    public override string MakeSound()
    {
        return "Гав-гав!";
    }
}

public class Cat : Animal
{
    public Cat(string name) : base(name) { }

    public override string MakeSound()
    {
        return "Мяу!";
    }
}

public class Bird : Animal
{
    public Bird(string name) : base(name) { }

    public override string MakeSound()
    {
        return "Чирик-чирик!";
    }

    // Дополнительный метод только для птиц
    public string Fly()
    {
        return "Лечу в небе!";
    }
}