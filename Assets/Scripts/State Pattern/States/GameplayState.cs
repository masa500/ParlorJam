using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameplayState : IState
{

    private readonly float _gameplayDuration;
    private readonly GameObject _ghostSpawner;
    private readonly GameObject _seekCursor;
    private readonly Round _round;
    private int ghostsCaught = 0;
    private List<Ghost> _ghosts;

    public GameplayState(float gameplayDuration, GameObject ghostSpawner, Round round, GameObject seekCursor)
    {
        _gameplayDuration = gameplayDuration;
        _ghostSpawner = ghostSpawner;
        _round = round;
        _seekCursor = seekCursor;
    }

    public async UniTask<GameStateResult> DoAction(object data)
    {
        _ghosts = new List<Ghost>();

        _seekCursor.SetActive(true);

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
