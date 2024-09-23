namespace AstarMaze.App.Domain.Interfaces;

public interface IObserver
{
    void Update(ISubject subject);
}