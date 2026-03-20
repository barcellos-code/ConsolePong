using TickService;

namespace ConsoleTickService;

internal class ConsoleTickService : ITickService
{
    private const int Milliseconds = 50;

    public event Action? OnTick;

    private Thread? _tickRoutineThread;

    public ConsoleTickService()
    {
        StartTick();
    }

    private void StartTick()
    {
        _tickRoutineThread = new Thread(TickRoutine);
        _tickRoutineThread.Start();
    }

    private void TickRoutine()
    {
        while (true)
        {
            OnTick?.Invoke();
            Thread.Sleep(Milliseconds);
        }
    }
}
