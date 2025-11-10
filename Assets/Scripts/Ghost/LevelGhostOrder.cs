using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelGhostOrder", menuName = "Scriptable Objects/LevelGhostOrder")]
public class LevelGhostOrder : ScriptableObject
{
    [SerializeField] private bool easyLevel;
    [SerializeField] private List<GhostRoundOrder> _levelGhosts;

    public bool EasyLevel { get => easyLevel; set => easyLevel = value; }
    public List<GhostRoundOrder> LevelGhosts { get => _levelGhosts; set => _levelGhosts = value; }

    public List<GhostType> getGhostByRound(int round)
    {

        return _levelGhosts[round - 1].Ghosts;
    }
}
