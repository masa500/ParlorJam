using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GhostRoundOrder
{

    [SerializeField] private List<GhostType> _ghosts;

    public List<GhostType> Ghosts { get => _ghosts; set => _ghosts = value; }
}
