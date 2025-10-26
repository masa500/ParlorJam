public class GameStateResult
{
    public readonly int NextStateId;
    public readonly object ResultData;

    public GameStateResult(int nextStateId, object resultData = null)
    {
        NextStateId = nextStateId;
        ResultData = resultData;
    }
}
