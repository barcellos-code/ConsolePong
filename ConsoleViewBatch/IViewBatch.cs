namespace ConsoleViewBatch;

public interface IViewBatch
{
    void QueueForDraw(DrawBatchParameters parameters);
    void UnqueueForDraw(int instanceGUID);
    void Start();
    void Stop();
}
