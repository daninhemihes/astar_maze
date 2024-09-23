namespace AstarMaze.App.Domain.Interfaces;

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}