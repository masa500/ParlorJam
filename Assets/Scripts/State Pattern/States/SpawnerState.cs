using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using NewgroundsUnityAPIHelper.Helper.Scripts;
using TMPro;
using UnityEngine;

public class SpawnerState : IState
{
    private readonly float _animDuration;
    private readonly RectTransform _fadeObject;
    private readonly SpriteMask _lineMask;
    private readonly GhostSpawner _ghostSpawner;
    private readonly GameObject _tutorialUI;
    private readonly GameObject _gameplayUI;
    private readonly Round _round;
    private readonly TextMeshProUGUI _textRound;
    private readonly GameObject _linesContainer;
    private readonly GameplayMode _gameplayMode;
    public SpawnerState(float animDuration, RectTransform fadeObject, GhostSpawner ghostSpawner, Round round, GameObject tutorialUI,
        GameObject gameplayUI, SpriteMask lineMask, GameObject linesContainer, GameplayMode gameplayMode, TextMeshProUGUI textRound)
    {
        _animDuration = animDuration;
        _fadeObject = fadeObject;
        _ghostSpawner = ghostSpawner;
        _round = round;
        _linesContainer = linesContainer;
        _tutorialUI = tutorialUI;
        _gameplayUI = gameplayUI;
        _lineMask = lineMask;
        _gameplayMode = gameplayMode;
        _textRound = textRound;
    }

    public async UniTask<GameStateResult> DoAction(object data)
    {

        if (_round.current > 15)
            return new GameStateResult(GameConfiguration.VictoryState, data);

        if (_round.current >= 8)
        {
            if (_gameplayMode._easyMode)
            {
                NewgroundsAPIHelper.Instance.IsUserLoggedIn(isLoggedIn =>
                {
                    if (!NewgroundsAPIHelper.Instance.IsMedalUnlocked((int)NGMedalsEnum.Ghost))
                        NewgroundsAPIHelper.Instance.UnlockMedal((int)NGMedalsEnum.Ghost);
                });
            }
            else
            {
                NewgroundsAPIHelper.Instance.IsUserLoggedIn(isLoggedIn =>
                {
                    if (!NewgroundsAPIHelper.Instance.IsMedalUnlocked((int) NGMedalsEnum.BahiaGhost))
                        NewgroundsAPIHelper.Instance.UnlockMedal((int) NGMedalsEnum.BahiaGhost);
                });
            }
        }

        List<GameObject> childrenToDestroy = new List<GameObject>();

        _lineMask.sprite = null;

        foreach (Transform child in _ghostSpawner.transform)
        {
            childrenToDestroy.Add(child.gameObject);
        }

        foreach (GameObject child in childrenToDestroy)
        {
            GameObject.Destroy(child);
        }

        childrenToDestroy = new List<GameObject>();

        foreach (Transform child in _linesContainer.transform)
        {
            childrenToDestroy.Add(child.gameObject);
        }

        foreach (GameObject child in childrenToDestroy)
        {
            GameObject.Destroy(child);
        }

        _ghostSpawner.SpawnGhost(_round.current, _gameplayMode._easyMode);

        _tutorialUI.SetActive(false);
        _gameplayUI.SetActive(true);

        _fadeObject.gameObject.SetActive(true);
        _fadeObject.DOSizeDelta(new Vector2(1200f, 800f), _animDuration).SetEase(Ease.InOutSine);

        await UniTask.Delay(TimeSpan.FromSeconds(_animDuration));
        
        _textRound.text = _round.current + "/15";

        return new GameStateResult(GameConfiguration.HideState, data);
    }
}
