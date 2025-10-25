using UnityEngine;

public class ChangeHideMovementGhost : Ghost
{
    public override void HandleGhostHide()
    {
        _dir = -_dir;
    }
}
