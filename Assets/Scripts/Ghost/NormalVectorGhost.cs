using UnityEngine;

public class NormalVectorGhost : Ghost
{
    public override void HandleGhostHide()
    {
        
    }

    protected new void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal.normalized;

        _dir = normal.normalized;

        lookAtDirection();
    }
}
