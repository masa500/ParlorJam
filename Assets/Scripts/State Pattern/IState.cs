using Cysharp.Threading.Tasks;

public interface IState
{
    UniTask<GameStateResult> DoAction(object data);
}
