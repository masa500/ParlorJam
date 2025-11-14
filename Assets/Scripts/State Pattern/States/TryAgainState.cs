using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using NewgroundsUnityAPIHelper.Helper.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TryAgainState : IState
{

    private readonly RectTransform _fadeObject;
    private readonly float _fadeDuration;
    private bool _buttonPressed = false;
    private readonly Button _tryBtn;
    private readonly GameObject _tryAgainUI;
    private readonly TextMeshProUGUI _textRound;
    private readonly Round _round;

    public TryAgainState(RectTransform fadeObject, float fadeDuration, Button tryBtn, GameObject tryAgainUI, Round round, TextMeshProUGUI textRound)
    {
        _fadeObject = fadeObject;
        _fadeDuration = fadeDuration;
        _tryBtn = tryBtn;
        _tryAgainUI = tryAgainUI;
        _round = round;
        _textRound = textRound;
        _tryBtn.onClick.AddListener(HandleButtonPressed);
    }

    private void HandleButtonPressed()
    {
        _buttonPressed = true;
    }
    
    public async UniTask<GameStateResult> DoAction(object data)
    {
        _tryAgainUI.SetActive(true);

        _tryBtn.interactable = true;

        try
        {
            NewgroundsAPIHelper.Instance.IsUserLoggedIn(isLoggedIn =>
            {
                if (!NewgroundsAPIHelper.Instance.IsMedalUnlocked((int)NGMedalsEnum.Scaryyyyyyyy))
                    NewgroundsAPIHelper.Instance.UnlockMedal((int)NGMedalsEnum.Scaryyyyyyyy);
            });
        }
        catch (Exception e)
        {
            Debug.Log("Error with the NG Api " + e);
        }

        while (!_buttonPressed)
        {
            await UniTask.Yield();
        }

        _tryBtn.interactable = false;

        _fadeObject.DOSizeDelta(new Vector2(1f, 1f), _fadeDuration).SetEase(Ease.InOutSine);

        await UniTask.Delay(TimeSpan.FromSeconds(_fadeDuration));

        _fadeObject.gameObject.SetActive(false);

        _tryAgainUI.SetActive(false);

        _buttonPressed = false;

        _round.current = 1;

        _textRound.text = "1/15";

        return new GameStateResult(GameConfiguration.SpawnerState, data);
    }
}
