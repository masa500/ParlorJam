using Cysharp.Threading.Tasks;
using UnityEngine;

public class VictoryState : IState
{
    private readonly RectTransform _transitionFadeObject;
    private readonly GameObject _victoryUI;

    public VictoryState(RectTransform transitionFadeObject, GameObject victoryUI)
    {
        _victoryUI = victoryUI;
        transitionFadeObject = _transitionFadeObject;
    }
    
    public async UniTask<GameStateResult> DoAction(object data)
    {
        _victoryUI.SetActive(true);

        _transitionFadeObject.sizeDelta = new Vector2(1200f,800f);

        await UniTask.Yield();

        return new GameStateResult(GameConfiguration.VictoryState, data);
    }
}
