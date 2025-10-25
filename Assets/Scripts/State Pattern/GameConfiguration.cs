using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class GameConfiguration
{
    private int InitialState;
    public const int MainMenuState = 0;
    public const int TutorialState = 1;
    public const int SpawnerState = 2;
    public const int HideState = 3;
    public const int GameplayState = 4;
    public const int WinState = 5;
    public const int LoseState = 6;
    public const int TryAgainState = 7;
    public const int VictoryState = 8;
    public int currentRound = 1;
    public UnityAction OnHide;

    private readonly Dictionary<int, IState> _states;

    public GameConfiguration()
    {
        _states = new Dictionary<int, IState>();
    }

    public void AddInitialState(int id, IState state)
    {
        _states.Add(id, state);
        InitialState = id;
    }

    public void AddState(int id, IState state)
    {
        _states.Add(id, state);
    }

    public IState GetState(int stateId)
    {
        Assert.IsTrue(_states.ContainsKey(stateId), $"State with id {stateId} do not exit");
        return _states[stateId];
    }

    public IState GetInitialState()
    {
        return GetState(InitialState);
    }

    public void InvokeOnHide()
    {
        OnHide?.Invoke();
    }
}

