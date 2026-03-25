using System;
using System.IO;
using ConsoleViewBatch;
using MatchPresenter;

namespace ConsoleMatchView
{
    public class MatchView : IMatchView
    {
        private readonly IViewBatch _viewBatch;

        private int _winningPlayerId;
        private int _screenWidth;
        private int _screenHeight;

        public MatchView(IViewBatch viewBatch)
        {
            _viewBatch = viewBatch;
        }

        public void DrawMatchEnded(int winningPlayerId, int screenWidth, int screenHeight)
        {
            _winningPlayerId = winningPlayerId;
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            var drawBatchParameters = new DrawBatchParameters(
                instanceGUID: nameof(MatchView).GetHashCode(),
                drawAction: DrawAction,
                isOneTimeDraw: false
            );

            _viewBatch.QueueForDraw(drawBatchParameters);
        }

        private void DrawAction()
        {
            var left = _screenWidth / 2;
            var top = _screenHeight - 3;

            try
            {
                Console.SetCursorPosition(left, top);
                Console.Write($"Player {_winningPlayerId} won!");
            }
            catch (IOException) { }
        }
    }
}
