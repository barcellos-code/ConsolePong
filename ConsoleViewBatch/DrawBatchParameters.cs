namespace ConsoleViewBatch;

public readonly struct DrawBatchParameters(int instanceGUID, Action drawAction, bool isOneTimeDraw)
{
    public readonly int InstanceGUID = instanceGUID;
    public readonly Action DrawAction = drawAction;
    public readonly bool IsOneTimeDraw = isOneTimeDraw;
}
