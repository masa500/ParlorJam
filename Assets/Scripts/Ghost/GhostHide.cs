using Unity.VisualScripting;
using UnityEngine;

public class GhostHide : Ghost
{
    public override void HandleGhostHide()
    {
        _speed = 100f;
    }
}
