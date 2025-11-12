using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VictoryState : IState
{
    private readonly GameObject _victoryUI;
    private readonly GameObject _menuUI;
    private readonly RectTransform _fadeObject;
    private readonly Button _easyModeButton;
    private readonly Button _hardModeButton;
    private readonly Button _creditsButton;
    private readonly Button _winButton;
    private readonly Round _round;
    private readonly float _fadeDuration;
    private readonly GameObject _gameplayUI;
    private readonly GameplayMode _gameplayMode;
    private bool _buttonPressed = false;


    public VictoryState(GameObject victoryUI, RectTransform fadeObject, Button easyModeButton, Button hardModeButton, Button creditsButton, Button winButton,
        GameObject menuUI, float fadeDuration, Round round, GameObject gameplayUI, GameplayMode gameplayMode)
    {
        _victoryUI = victoryUI;
        _fadeObject = fadeObject;
        _creditsButton = creditsButton;
        _easyModeButton = easyModeButton;
        _hardModeButton = hardModeButton;
        _winButton = winButton;
        _menuUI = menuUI;
        _fadeDuration = fadeDuration;
        _round = round;
        _gameplayUI = gameplayUI;
        _gameplayMode = gameplayMode;

        _winButton.onClick.AddListener(HandleButtonPressed);
    }

    private void HandleButtonPressed()
    {
        Debug.Log("MenuState: Button Pressed");
        _buttonPressed = true;
    }
    
    public async UniTask<GameStateResult> DoAction(object data)
    {
        _victoryUI.SetActive(true);

        _fadeObject.gameObject.SetActive(true);

        _fadeObject.DOSizeDelta(new Vector2(1200f, 800f), _fadeDuration).SetEase(Ease.InOutSine);

        while (!_buttonPressed)
        {
            await UniTask.Yield();
        }

        _fadeObject.DOSizeDelta(new Vector2(1f, 1f), _fadeDuration).SetEase(Ease.InOutSine);

        _winButton.interactable = false;

        await UniTask.Delay(TimeSpan.FromSeconds(_fadeDuration + 0.5f));

        _menuUI.SetActive(true);

        _gameplayUI.SetActive(false);

        _victoryUI.SetActive(false);

        _fadeObject.DOSizeDelta(new Vector2(1200f, 800f), _fadeDuration).SetEase(Ease.InOutSine);

        await UniTask.Delay(TimeSpan.FromSeconds(_fadeDuration));

        _easyModeButton.interactable = true;
        _hardModeButton.interactable = true;
        _creditsButton.interactable = true;
        
        _winButton.interactable = true;

        _round.current = 1;

        _buttonPressed = false;

        return new GameStateResult(GameConfiguration.MainMenuState, data);
    }
}
