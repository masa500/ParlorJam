using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameplayState : IState
{

    private readonly float _gameplayDuration;
    private readonly GameObject _ghostSpawner;
    private readonly GameObject _seekCursor;
    private readonly Round _round;
    private readonly InputReader _controls;
    private readonly GameObject _soundPrefab;
    private readonly AudioClip _initSound;
    private readonly AudioClip _clockSound;
    private readonly TextMeshProUGUI _textRound;
    private readonly TextMeshProUGUI _textTimer;
    private int ghostsCaught = 0;
    private List<Ghost> _ghosts;

    public GameplayState(float gameplayDuration, GameObject ghostSpawner, Round round, GameObject seekCursor, InputReader controls,
        GameObject soundPrefab, AudioClip initSound, TextMeshProUGUI textRound, TextMeshProUGUI textTimer, AudioClip clockSound)
    {
        _gameplayDuration = gameplayDuration;
        _ghostSpawner = ghostSpawner;
        _round = round;
        _seekCursor = seekCursor;
        _controls = controls;
        _soundPrefab = soundPrefab;
        _initSound = initSound;
        _textRound = textRound;
        _textTimer = textTimer;
        _clockSound = clockSound;
    }

    public async UniTask<GameStateResult> DoAction(object data)
    {

        _ghosts = new List<Ghost>();

        _controls.EnableControls();

        _seekCursor.SetActive(true);

        var soundPre = GameObject.Instantiate(_soundPrefab);

        soundPre.GetComponent<AudioSource>().clip = _initSound;

        soundPre.GetComponent<AudioSource>().Play();

        var timerValue = _gameplayDuration;

        var lastSecondPlayed = (int)timerValue + 1;

        DOTween.To(() => timerValue, x => timerValue = x, 0, _gameplayDuration).SetEase(Ease.Linear)
            .OnUpdate(() => {
                
                int seconds = (int)Mathf.Ceil(timerValue);
                _textTimer.text = seconds.ToString();

                if (seconds < lastSecondPlayed)
                {
                    var soundPre = GameObject.Instantiate(_soundPrefab);

                    soundPre.GetComponent<AudioSource>().clip = _clockSound;

                    soundPre.GetComponent<AudioSource>().Play();

                    lastSecondPlayed = seconds;
                }
            });

        foreach (Transform child in _ghostSpawner.transform)
        {
            Ghost _ghost = child.gameObject.GetComponent<Ghost>();

            _ghosts.Add(_ghost);
        }

        await UniTask.Delay(TimeSpan.FromSeconds(_gameplayDuration));

        foreach (Ghost ghost in _ghosts)
        {
            ghostsCaught += ghost.IsCaught? 1: 0;
        }

        _seekCursor.SetActive(false);

        if (ghostsCaught >= _ghosts.Count)
        {
            Debug.Log("GameplayState: All ghosts caught");
            ghostsCaught = 0;
            _round.current++;
            _textRound.text = _round.current + "/15";
            return new GameStateResult(GameConfiguration.WinState, data);
        }
        else
        {
            Debug.Log("GameplayState: Some ghosts missed");
            ghostsCaught = 0;
            return new GameStateResult(GameConfiguration.LoseState, data);
        }

    }
}
