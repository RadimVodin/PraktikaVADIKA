namespace CSharpPracticum.Common;

/// <summary>
/// Единый контракт задания практикума.
/// </summary>
public interface ITask
{
    string Title { get; }
    string Description { get; }
    string[] Requirements { get; }
    void Run();
}