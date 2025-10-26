using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
    private readonly GameObject _linesContainer;
    public SpawnerState(float animDuration, RectTransform fadeObject, GhostSpawner ghostSpawner, Round round, GameObject tutorialUI, GameObject gameplayUI, SpriteMask lineMask, GameObject linesContainer)
    {
        _animDuration = animDuration;
        _fadeObject = fadeObject;
        _ghostSpawner = ghostSpawner;
        _round = round;
        _linesContainer = linesContainer;
        _tutorialUI = tutorialUI;
        _gameplayUI = gameplayUI;
        _lineMask = lineMask;
    }

    public async UniTask<GameStateResult> DoAction(object data)
    {

        if(_round.current >15)
            return new GameStateResult(GameConfiguration.VictoryState, data);

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

        _ghostSpawner.SpawnGhost(_round.current);

        _tutorialUI.SetActive(false);
        _gameplayUI.SetActive(true);

        _fadeObject.gameObject.SetActive(true);
        _fadeObject.DOSizeDelta(new Vector2(1200f, 800f), _animDuration).SetEase(Ease.InOutSine);

        await UniTask.Delay(TimeSpan.FromSeconds(_animDuration));

        return new GameStateResult(GameConfiguration.HideState, data);
    }
}
