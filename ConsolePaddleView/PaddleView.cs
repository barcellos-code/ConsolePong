using System;
using System.IO;
using ConsoleViewBatch;
using PaddlePresenter;

namespace ConsolePaddleView
{
    internal class PaddleView : IPaddleView
    {
        private readonly IViewBatch _viewBatch;

        private int _size;
        private int _posX;
        private int _posY;

        public PaddleView(IViewBatch viewBatch)
        {
            _viewBatch = viewBatch;
        }

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
}
