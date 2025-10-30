using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HideState : IState
{
    private readonly float _idleDuration;
    private readonly float _hideDuration;
    private readonly float _blackOutDuration;
    private readonly SpriteRenderer _fadeMask;
    private readonly GameObject _ghostSpawner;
    private readonly InputReader _controls;
    private readonly float _gameplayDuration;
    private readonly TextMeshProUGUI _textTimer;
    private List<Ghost> _ghosts;

    public HideState(float idleDuration, float hideDuration, float blackOutDuration, SpriteRenderer fadeMask, GameObject ghostSpawner, InputReader controls,
        TextMeshProUGUI textTimer, float gameplayDuration)
    {
        _idleDuration = idleDuration;
        _hideDuration = hideDuration;
        _blackOutDuration = blackOutDuration;
        _fadeMask = fadeMask;
        _ghostSpawner = ghostSpawner;
        _controls = controls;
        _textTimer = textTimer;
        _gameplayDuration = gameplayDuration;
    }

    public async UniTask<GameStateResult> DoAction(object data)
    {

        _textTimer.text = _gameplayDuration.ToString();

        _ghosts = new List<Ghost>();

        _controls.DisableControls();

        foreach (Transform child in _ghostSpawner.transform)
        {
            if (child.TryGetComponent<Ghost>(out var ghost))
            {
                _ghosts.Add(ghost);
            }
        }

        await UniTask.Delay(TimeSpan.FromSeconds(_idleDuration));

        foreach (var ghost in _ghosts)
        {
            ghost.HandleGhostHide();
        }

        DOTween.To(() => _fadeMask.color, x => _fadeMask.color = x, new Color(0f, 0f, 0f, 1f), _hideDuration).SetEase(Ease.InOutSine);

        await UniTask.Delay(TimeSpan.FromSeconds(_hideDuration));

        await UniTask.Delay(TimeSpan.FromSeconds(_blackOutDuration));

        foreach (var ghost in _ghosts)
        {
            ghost.StopMovement();
        }

        return new GameStateResult(GameConfiguration.GameplayState, data);
    }
}
