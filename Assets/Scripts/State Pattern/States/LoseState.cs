using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class LoseState: IState
{
    private GameObject _ghostSpawner;
    private readonly float _restDuration;
    private readonly float _fadeMaskDuration;
    private readonly SpriteRenderer _fadeMask;
    private readonly GameObject _soundPrefab;
    private readonly AudioClip _loseSound;
    private List<Ghost> _ghosts;

    public LoseState(GameObject ghostSpawner, float restDuration, float fadeMaskDuration, SpriteRenderer fadeMask, GameObject soundPrefab, AudioClip loseSound)
    {
        _ghostSpawner = ghostSpawner;
        _restDuration = restDuration;
        _fadeMaskDuration = fadeMaskDuration;
        _fadeMask = fadeMask;
        _soundPrefab = soundPrefab;
        _loseSound = loseSound;
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

        DOTween.To(() => _fadeMask.color, x => _fadeMask.color = x, new Color(0f, 0f, 0f, 0f), _fadeMaskDuration).SetEase(Ease.InOutSine);

        await Task.Delay(TimeSpan.FromSeconds(_fadeMaskDuration));

        await Task.Delay(TimeSpan.FromSeconds(_restDuration));

        foreach (var ghost in _ghosts)
        {
            await ghost.CountAvailableGhosts();

            await Task.Delay(TimeSpan.FromSeconds(_restDuration));
        }

        var soundPre = GameObject.Instantiate(_soundPrefab);

        soundPre.GetComponent<AudioSource>().clip = _loseSound;

        soundPre.GetComponent<AudioSource>().Play();

        await Task.Delay(TimeSpan.FromSeconds(_restDuration));
        
        return new GameStateResult(GameConfiguration.TryAgainState, data);
    }
}
