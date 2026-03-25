using System;
using System.IO;
using BallPresenter;
using ConsoleViewBatch;

namespace ConsoleBallView
{
    internal class BallView : IBallView
    {
        private readonly IViewBatch _viewBatch;

        private int _posX;
        private int _posY;

        public BallView(IViewBatch viewBatch)
        {
            _viewBatch = viewBatch;
        }

        public void DrawBall(int posX, int posY)
        {
            _posX = posX;
            _posY = posY;

            var drawBatchParameters = new DrawBatchParameters(
                instanceGUID: nameof(BallView).GetHashCode(),
                drawAction: DrawAction,
                isOneTimeDraw: false
            );

            _viewBatch.QueueForDraw(drawBatchParameters);
        }

        private void DrawAction()
        {
            try
            {
                Console.SetCursorPosition(_posX, _posY);
                Console.Write("O");
            }
            catch (IOException) { }
        }
    }
}
