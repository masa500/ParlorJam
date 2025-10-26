using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public abstract class Ghost : MonoBehaviour
{
    [SerializeField] protected int id;
    [SerializeField] protected float _speed = 100f;
    [SerializeField] protected AnimationClip _baseAnim;
    [SerializeField] protected AnimationClip _animIdle;
    [SerializeField] protected SpriteRenderer _plusOneSprite;
    [SerializeField] protected SpriteRenderer _caughtSprite;
    [SerializeField] protected AudioClip _caughtSound;
    [SerializeField] protected GameObject _soundPrefab;
    protected Animator _anim;
    protected Vector2 _dir;
    protected Rigidbody2D _rb2d;
    private bool isCaught;
    protected int Id { get => id; }
    public bool IsCaught { get => isCaught; set => isCaught = value; }

    void Start()
    {
        StartGhostVectorMovement();
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        isCaught = false;

        _anim.Play(_baseAnim.name);
    }

    void FixedUpdate()
    {
        _rb2d.linearVelocity = Vector2.zero;
        _rb2d.AddForce(_dir * _speed, ForceMode2D.Force);
    }

    protected void StartGhostVectorMovement()
    {
        Vector2 randomDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        _dir = randomDirection;
        lookAtDirection();
    }
    
    protected void lookAtDirection()
    {
        transform.localScale = new Vector3(_dir.x >= 0 ? -1 : 1, 1, 1);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal.normalized;

        if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            _dir.x = -_dir.x;
        else
            _dir.y = -_dir.y;

        lookAtDirection();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Seek"))
        {
            _dir = Vector2.zero;
            _anim.Play(_animIdle.name);
            CaughtSprite();
            isCaught = true;
        }
    }

    private void CaughtSprite() {

        if (isCaught) return;

        _caughtSprite.gameObject.SetActive(true);

        _caughtSprite.gameObject.transform.DOLocalMoveY(0.5f, 0.75f).OnComplete(() => _caughtSprite.gameObject.SetActive(false));

        var soundPre = Instantiate(_soundPrefab);

        soundPre.GetComponent<AudioSource>().clip = _caughtSound;

        soundPre.GetComponent<AudioSource>().Play();
    }

    public abstract void HandleGhostHide();

    public void StopMovement()
    {
        _dir = Vector2.zero;
        _speed = 0f;
    }

    public async UniTask CountAvailableGhosts()
    {
        if (!isCaught) return;

        if (transform.localScale.x == -1f)
            _plusOneSprite.transform.localScale = new Vector3(-1f,1f,1f);

        _plusOneSprite.color = new Color(1f, 1f, 1f, 1f);

        if(this.gameObject.transform.position.y < 0f)
            _plusOneSprite.transform.DOLocalMoveY(0.5f, 1f);
        else
            _plusOneSprite.transform.DOLocalMoveY(-0.5f, 1f);

        await UniTask.Delay(TimeSpan.FromSeconds(1f));
    }
}
