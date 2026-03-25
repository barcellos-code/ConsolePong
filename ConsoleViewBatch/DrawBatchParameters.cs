using System;

namespace ConsoleViewBatch
{
    public readonly struct DrawBatchParameters
    {
        public readonly int InstanceGUID;
        public readonly Action DrawAction;
        public readonly bool IsOneTimeDraw;

        public DrawBatchParameters(int instanceGUID, Action drawAction, bool isOneTimeDraw)
        {
            InstanceGUID = instanceGUID;
            DrawAction = drawAction;
            IsOneTimeDraw = isOneTimeDraw;
        }
    }
}
