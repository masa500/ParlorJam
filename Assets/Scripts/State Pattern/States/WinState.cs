using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class WinState : IState
{
    private readonly GameObject _ghostSpawner;
    private readonly float _restDuration;
    private readonly float _fadeMaskDuration;
    private readonly SpriteRenderer _fadeMask;
    private readonly RectTransform _fadeObject;
    private readonly float _fadeDuration;
    private readonly GameObject _soundPrefab;
    private readonly AudioClip _winSound;
    private List<Ghost> _ghosts;

    public WinState(GameObject ghostSpawner, float restDuration, float fadeMaskDuration, SpriteRenderer fadeMask, RectTransform fadeObject,
        float fadeDuration, GameObject soundPrefab, AudioClip winSound)
    {
        _ghostSpawner = ghostSpawner;
        _restDuration = restDuration;
        _fadeMaskDuration = fadeMaskDuration;
        _fadeMask = fadeMask;
        _fadeObject = fadeObject;
        _fadeDuration = fadeDuration;
        _soundPrefab = soundPrefab;
        _winSound = winSound;
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

        soundPre.GetComponent<AudioSource>().clip = _winSound;

        soundPre.GetComponent<AudioSource>().Play();

        await Task.Delay(TimeSpan.FromSeconds(_restDuration));

        _fadeObject.DOSizeDelta(new Vector2(1f, 1f), _fadeDuration).SetEase(Ease.InOutSine);

        await Task.Delay(TimeSpan.FromSeconds(_fadeDuration));

        _fadeObject.gameObject.SetActive(false);
        
        return new GameStateResult(GameConfiguration.SpawnerState, data);
    }
    
}
