using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VictoryState : IState
{
    private readonly RectTransform _transitionFadeObject;
    private readonly GameObject _victoryUI;

    public VictoryState(RectTransform transitionFadeObject, GameObject victoryUI)
    {
        _victoryUI = victoryUI;
        transitionFadeObject = _transitionFadeObject;
    }
    
    public async Task<GameStateResult> DoAction(object data)
    {
        _victoryUI.SetActive(true);

        _transitionFadeObject.sizeDelta = new Vector2(1200f,800f);

        await Task.Yield();

        return new GameStateResult(GameConfiguration.VictoryState, data);
    }
}
