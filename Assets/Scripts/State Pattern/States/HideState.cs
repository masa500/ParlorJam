using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class HideState : IState
{
    private readonly float _idleDuration;
    private readonly float _hideDuration;
    private readonly float _blackOutDuration;
    private readonly SpriteRenderer _fadeMask;
    private readonly GameObject _ghostSpawner;
    private List<Ghost> _ghosts;

    public HideState(float idleDuration, float hideDuration, float blackOutDuration, SpriteRenderer fadeMask, GameObject ghostSpawner)
    {
        _idleDuration = idleDuration;
        _hideDuration = hideDuration;
        _blackOutDuration = blackOutDuration;
        _fadeMask = fadeMask;
        _ghostSpawner = ghostSpawner;
    }

    public async Task<GameStateResult> DoAction(object data)
    {
        _ghosts = new List<Ghost>();

        foreach (Transform child in _ghostSpawner.transform)
        {
            if (child.TryGetComponent<Ghost>(out var ghost))
            {
                _ghosts.Add(ghost);
            }
        }

        await Task.Delay(TimeSpan.FromSeconds(_idleDuration));

        foreach (var ghost in _ghosts)
        {
            ghost.HandleGhostHide();
        }

        DOTween.To(() => _fadeMask.color, x => _fadeMask.color = x, new Color(0f, 0f, 0f, 1f), _hideDuration).SetEase(Ease.InOutSine);

        await Task.Delay(TimeSpan.FromSeconds(_hideDuration));

        await Task.Delay(TimeSpan.FromSeconds(_blackOutDuration));

        foreach (var ghost in _ghosts)
        {
            ghost.StopMovement();
        }

        return new GameStateResult(GameConfiguration.GameplayState, data);
    }
}
