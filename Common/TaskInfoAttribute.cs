namespace CSharpPracticum.Common;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TaskInfoAttribute : Attribute
{
    public int Number { get; }
    public int Level { get; }

    public TaskInfoAttribute(int number, int level)
    {
        Number = number;
        Level = level;
    }
}