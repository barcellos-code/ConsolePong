using PaddlesController;

namespace ConsolePaddlesInput;

internal class PaddlesInputService : IPaddlesInputService
{
    private const float PollRate = 60; // input polls per second
    private const int Milliseconds = (int)(1 / PollRate * 1000);

    public event Action<int, PaddlesInputDirection>? OnInput;

    private Thread? _pollInputThread;
    private bool _isActive;

    public void StartInputHandling()
    {
        _isActive = true;
        _pollInputThread = new Thread(PollInputRoutine);
        _pollInputThread.Start();
    }

    public void StopInputHandling()
        => _isActive = false;

    private void PollInputRoutine()
    {
        while (_isActive)
        {
            if (TryPollInput(out var paddleIndex, out var inputDirection))
                OnInput?.Invoke(paddleIndex, inputDirection);
            
            Thread.Sleep(Milliseconds);
        }
    }

    private static bool TryPollInput(out int paddleIndex, out PaddlesInputDirection inputDirection)
    {
        paddleIndex = -1;
        inputDirection = default;

        if (!Console.KeyAvailable)
            return false;
        
        var keyInfo = Console.ReadKey(intercept: true);

        if (keyInfo.Key == ConsoleKey.W)
        {
            paddleIndex = 0;
            inputDirection = PaddlesInputDirection.Down;
            return true;
        }

        if (keyInfo.Key == ConsoleKey.S)
        {
            paddleIndex = 0;
            inputDirection = PaddlesInputDirection.Up;
            return true;
        }

        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            paddleIndex = 1;
            inputDirection = PaddlesInputDirection.Down;
            return true;
        }

        if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            paddleIndex = 1;
            inputDirection = PaddlesInputDirection.Up;
            return true;
        }

        return false;
    }
}
