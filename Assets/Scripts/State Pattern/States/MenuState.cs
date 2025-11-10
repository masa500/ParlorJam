using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuState: IState
{
    private readonly RectTransform _fadeObject;
    private readonly Button _easyModeButton;
    private readonly Button _hardModeButton;
    private readonly Button _creditsButton;
    private readonly Button _nextButton;
    private readonly float _fadeDuration;
    private readonly GameObject _menuUI;
    private readonly GameObject _tutorialUI;
    private readonly GameplayMode _gameplayMode;
    private bool _buttonPressed = false;

    public MenuState(GameObject menuUI, GameObject tutorialUI, RectTransform fadeObject, float fadeDuration, Button easyModeButton,
        Button creditsButton, Button nextButton, Button hardModeButton, GameplayMode gameplayMode)
    {
        _menuUI = menuUI;
        _tutorialUI = tutorialUI;
        _fadeObject = fadeObject;
        _fadeDuration = fadeDuration;
        _easyModeButton = easyModeButton;
        _hardModeButton = hardModeButton;
        _creditsButton = creditsButton;
        _nextButton = nextButton;
        _gameplayMode = gameplayMode;

        _easyModeButton.onClick.AddListener(HandleButtonPressed);
        _hardModeButton.onClick.AddListener(HandleHardButtonPressed);
    }

    private void HandleButtonPressed()
    {
        Debug.Log("MenuState: Button Pressed");
        _buttonPressed = true;
        _gameplayMode._easyMode = true;
    }

    private void HandleHardButtonPressed()
    {
        Debug.Log("MenuState: Button Pressed");
        _buttonPressed = true;
        _gameplayMode._easyMode = false;
    }

    public async UniTask<GameStateResult> DoAction(object data)
    {
        while (!_buttonPressed)
        {
            await UniTask.Yield();
        }

        _easyModeButton.interactable = false;
        _hardModeButton.interactable = false;
        _creditsButton.interactable = false;
        _nextButton.interactable = false;

        _fadeObject.DOSizeDelta(new Vector2(1f, 1f), _fadeDuration).SetEase(Ease.InOutSine);

        await UniTask.Delay(TimeSpan.FromSeconds(_fadeDuration + 0.5f));

        _menuUI.SetActive(false);

        _tutorialUI.SetActive(true);

        _fadeObject.DOSizeDelta(new Vector2(1200f, 800f), _fadeDuration).SetEase(Ease.InOutSine);

        await UniTask.Delay(TimeSpan.FromSeconds(_fadeDuration));

        _nextButton.interactable = true;

        _buttonPressed = false;

        return new GameStateResult(GameConfiguration.TutorialState, data);
    }
}