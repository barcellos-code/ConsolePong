using ConsoleViewBatch;
using PaddlePresenter;

namespace ConsolePaddleView;

internal class PaddleView(IViewBatch viewBatch) : IPaddleView
{
    private readonly IViewBatch _viewBatch = viewBatch;

    private int _size;
    private int _posX;
    private int _posY;

    public void DrawPaddle(int size, int posX, int posY)
    {
        _size = size;
        _posX = posX;
        _posY = posY;

        var drawBatchParameters = new DrawBatchParameters(
            instanceGUID: HashCode.Combine(nameof(PaddleView), _posX),
            drawAction: DrawAction,
            isOneTimeDraw: false
        );

        _viewBatch.QueueForDraw(drawBatchParameters);
    }

    private void DrawAction()
    {
        for (int i = 0; i < _size; i++)
        {
            try
            {
                Console.SetCursorPosition(_posX, _posY + i);
                Console.Write("|");
            }
            catch (IOException) { }
        }
    }
}
