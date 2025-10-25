using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TryAgainState : IState
{

    private readonly RectTransform _fadeObject;
    private readonly float _fadeDuration;
    private bool _buttonPressed = false;
    private readonly Button _tryBtn;
    private readonly GameObject _tryAgainUI;
    private readonly Round _round;

    public TryAgainState(RectTransform fadeObject, float fadeDuration, Button tryBtn, GameObject tryAgainUI, Round round)
    {
        _fadeObject = fadeObject;
        _fadeDuration = fadeDuration;
        _tryBtn = tryBtn;
        _tryAgainUI = tryAgainUI;
        _round = round;
        _tryBtn.onClick.AddListener(HandleButtonPressed);
    }

    private void HandleButtonPressed()
    {
        _buttonPressed = true;
    }
    
    public async Task<GameStateResult> DoAction(object data)
    {
        _tryAgainUI.SetActive(true);

        _tryBtn.interactable = true;

        while (!_buttonPressed)
        {
            await Task.Yield();
        }

        _tryBtn.interactable = false;

        _fadeObject.DOSizeDelta(new Vector2(1f, 1f), _fadeDuration).SetEase(Ease.InOutSine);

        await Task.Delay(TimeSpan.FromSeconds(_fadeDuration));

        _fadeObject.gameObject.SetActive(false);

        _tryAgainUI.SetActive(false);

        _buttonPressed = false;

        _round.current = 1;

        return new GameStateResult(GameConfiguration.SpawnerState, data);
    }
}
