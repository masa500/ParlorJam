using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuState: IState
{
    private readonly RectTransform _fadeObject;
    private readonly Button _playButton;
    private readonly Button _creditsButton;
    private readonly Button _nextButton;
    private readonly float _fadeDuration;
    private readonly GameObject _menuUI;
    private readonly GameObject _tutorialUI;
    private bool _buttonPressed = false;

    public MenuState(GameObject menuUI, GameObject tutorialUI, RectTransform fadeObject, float fadeDuration, Button playButton, Button creditsButton, Button nextButton)
    {
        _menuUI = menuUI;
        _tutorialUI = tutorialUI;
        _fadeObject = fadeObject;
        _fadeDuration = fadeDuration;
        _playButton = playButton;
        _creditsButton = creditsButton;
        _nextButton = nextButton;

        _playButton.onClick.AddListener(HandleButtonPressed);
    }

    private void HandleButtonPressed()
    {
        Debug.Log("MenuState: Button Pressed");
        _buttonPressed = true;
    }

    public async Task<GameStateResult> DoAction(object data)
    {
        while (!_buttonPressed)
        {
            await Task.Yield();
        }

        _playButton.interactable = false;
        _creditsButton.interactable = false;
        _nextButton.interactable = false;

        _fadeObject.DOSizeDelta(new Vector2(1f, 1f), _fadeDuration).SetEase(Ease.InOutSine);

        await Task.Delay(TimeSpan.FromSeconds(_fadeDuration + 0.5f));

        _menuUI.SetActive(false);

        _tutorialUI.SetActive(true);

        _fadeObject.DOSizeDelta(new Vector2(1200f, 800f), _fadeDuration).SetEase(Ease.InOutSine);

        await Task.Delay(TimeSpan.FromSeconds(_fadeDuration));

        _nextButton.interactable = true;

        return new GameStateResult(GameConfiguration.TutorialState, data);
    }
}