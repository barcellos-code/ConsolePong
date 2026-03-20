using ConsoleViewBatch;
using StagePresenter;

namespace ConsoleStageView;

internal class StageView : IStageView
{
    private readonly IViewBatch _viewBatch;

    private int _stageWidth;
    private int _stageHeight;

    public StageView(IViewBatch viewBatch)
    {
        _viewBatch = viewBatch;

        try
        {
            Console.Title = "Console Pong";
            Console.CursorVisible = false;
        }
        catch (IOException) { }
    }

    public void DrawStage(int width, int height)
    {
        _stageWidth = width;
        _stageHeight = height;

        var drawBatchParameters = new DrawBatchParameters(
            instanceGUID: HashCode.Combine(nameof(StageView), _stageWidth, _stageHeight),
            drawAction: DrawAction,
            isOneTimeDraw: true
        );

        _viewBatch.QueueForDraw(drawBatchParameters);
    }

    private void DrawAction()
    {
        try
        {
#pragma warning disable CA1416 // Validate platform compatibility
            Console.SetWindowSize(_stageWidth, _stageHeight);
            Console.SetBufferSize(_stageWidth, _stageHeight);
#pragma warning restore CA1416 // Validate platform compatibility
        }
        catch (IOException) { }
    }
}
