namespace ConsoleViewBatch;

internal class ViewBatch : IViewBatch
{
    private const float FrameRate = 30; // frames per second
    private const int Milliseconds = (int)(1 / FrameRate * 1000);


    private readonly Dictionary<int, DrawBatchParameters> _instancesToDrawParametersDict = [];
    private Thread? _drawThread;
    private bool _isActive;

    public void QueueForDraw(DrawBatchParameters parameters)
        => _instancesToDrawParametersDict[parameters.InstanceGUID] = parameters;

    public void UnqueueForDraw(int instanceGUID)
    {
        if (!_instancesToDrawParametersDict.ContainsKey(instanceGUID))
            return;

        _instancesToDrawParametersDict.Remove(instanceGUID);
    }

    public void Start()
    {
        _isActive = true;
        _drawThread = new Thread(DrawRoutine);
        _drawThread.Start();
    }

    public void Stop()
        => _isActive = false;

    private void DrawRoutine()
    {
        while(_isActive)
        {
            try
            {
                Console.Clear();
            }
            catch (IOException) { }

            Draw();
            Thread.Sleep(Milliseconds);
        }
    }

    private void Draw()
    {
        for (var i = _instancesToDrawParametersDict.Count - 1; i >= 0; i--)
        {
            var instanceGUID = _instancesToDrawParametersDict.Keys.ElementAt(i);
            var parameters = _instancesToDrawParametersDict[instanceGUID];
            var drawAction = parameters.DrawAction;
            var isOneTimeDraw = parameters.IsOneTimeDraw;

            drawAction?.Invoke();

            if (isOneTimeDraw)
                UnqueueForDraw(instanceGUID);
        }
    }
}
