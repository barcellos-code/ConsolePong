using ConsoleViewBatch;
using PlayerPresenter;

namespace ConsolePlayerView;

internal class PlayerView(IViewBatch viewBatch) : IPlayerView
{
    private const float HorizontalOffsetFactor = 0.25f;

    private readonly IViewBatch _viewBatch = viewBatch;

    private int _playerId;
    private int _score;
    private int _screenWidth;
    private int _screenHeight;

    public void DrawPlayer(int playerId, int score, int screenWidth, int screenHeight)
    {
        _playerId = playerId;
        _score = score;
        _screenWidth = screenWidth;
        _screenHeight = screenHeight;

        var drawBatchParameters = new DrawBatchParameters(
            instanceGUID: HashCode.Combine(nameof(PlayerView), _playerId),
            drawAction: DrawAction,
            isOneTimeDraw: false
        );

        _viewBatch.QueueForDraw(drawBatchParameters);
    }

    private void DrawAction()
    {
        var left = _playerId == 1
            ? (int)(_screenWidth * HorizontalOffsetFactor)
            : (int)(_screenWidth * (1 - HorizontalOffsetFactor));
        
        var top = _screenHeight - 1;
        
        try
        {
            Console.SetCursorPosition(left, top);
            Console.Write($"Player {_playerId}: {_score}");
        }
        catch (IOException) { }
    }
}
