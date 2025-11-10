using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialState: IState
{
    private readonly TutorialLogic _tutorial;
    private readonly RectTransform _fadeObject;
    private readonly float _fadeDuration;
    private readonly Button _tutorialButton;
    private bool _buttonPressed = false;

    public TutorialState(TutorialLogic tutorial, RectTransform fadeObject, float fadeDuration, Button tutorialButton)
    {
        _tutorial = tutorial;
        _fadeObject = fadeObject;
        _fadeDuration = fadeDuration;
        _tutorialButton = tutorialButton;
        _tutorial.OnButtonPressed += HandleButtonPressed;
    }

    private void HandleButtonPressed()
    {
        _buttonPressed = true;
    }

    public async UniTask<GameStateResult> DoAction(object data)
    {
        while (!_buttonPressed)
        {
            await UniTask.Yield();
        }

        _tutorialButton.interactable = false;

        _fadeObject.DOSizeDelta(new Vector2(1f, 1f), _fadeDuration).SetEase(Ease.InOutSine);

        await UniTask.Delay(TimeSpan.FromSeconds(_fadeDuration));

        _fadeObject.gameObject.SetActive(false);

        _buttonPressed = false;

        return new GameStateResult(GameConfiguration.SpawnerState, data);
    }
}
