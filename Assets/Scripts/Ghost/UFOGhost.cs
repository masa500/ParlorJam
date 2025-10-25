using DG.Tweening;
using UnityEngine;

public class UFOGhost : Ghost
{
    public override void HandleGhostHide()
    {
        
    }

    void Start()
    {
        StartGhostVectorMovement();
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        IsCaught = false;

        _anim.Play(_baseAnim.name);

        DOTween.To(() => _speed, x => _speed = x, 250f, 5f).SetEase(Ease.OutQuad);
    }
}
