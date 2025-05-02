using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected ResourceController _resourceController;
    //자식의 스프라이트 오브젝트
    [SerializeField] protected SpriteRenderer _spriteRenderer;
    protected Rigidbody2D _rigidbody;

    public Animator Animator;

    [Header("Define")]
    [SerializeField] protected Define.State _state = Define.State.Idle;
    [SerializeField] protected Define.Dir _animDir = Define.Dir.None;

    [Header("Dir")]
    [SerializeField] protected Vector2 _moveDir = Vector2.zero;

    public Vector2 MoveDir { get { return _moveDir; } }


    [Header("Weapon Info")]
    [SerializeField] public WeaponHandler _weaponHandler;
    [SerializeField] protected SpriteRenderer _weaponRnderer;

    [SerializeField] private Transform _weaponPivot;
    protected Vector2 _attackDir = Vector2.right;
    protected bool _isAttacking;
    private float _timeSinceLastAttack = float.MaxValue;
    protected bool _isCanAttack = true;

    [Header("Stat Info")]
    // 점프 관련 변수 - 추후 스탯으로 넘겨야함
    protected float _height = 0f;
    protected float _verticalSpeed = 0f;
    protected bool _isGrounded = true;

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;
    private float knockbackTimer;

    private void Start()
    {
        Init();
        _resourceController = GetComponent<ResourceController>();
    }
    public void CreatWeapon(WeaponHandler _WeaponPrefab)
    {
        if (_weaponHandler != null)
        {
            Destroy(_weaponHandler.gameObject);
        }
        _weaponHandler = Instantiate(_WeaponPrefab, _spriteRenderer.transform);
        _weaponRnderer = _weaponHandler.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _weaponHandler.transform.parent = _weaponRnderer.transform;
        _weaponHandler.transform.position = _weaponPivot.position;
    }

    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            switch (_state)
            {
                case Define.State.Die:
                    UpdateDie();
                    break;
                case Define.State.Idle:
                    Animator.CrossFade("Idle", 0.1f);
                    break;
                case Define.State.Moving:
                    Animator.CrossFade("Run", 0.1f);
                    break;
                case Define.State.Skill:
                    Animator.CrossFade("Attack", 0.1f, -1, 0);
                    break;
            }
        }
    }

    void Update()
    {
        switch (State)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;
        }
        HandleAttackDelay();
    }

    private void FixedUpdate()
    {
        switch (State)
        {
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Knockback:
                UpdateKnockBack();
                break;
        }

        // 점프 관련연산
        UpdateJump();
    }


    protected void HandleAttackDelay()
    {
        if (_weaponHandler == null)
            return;

        if (_timeSinceLastAttack <= _weaponHandler.Delay)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        if (_isAttacking && _timeSinceLastAttack > _weaponHandler.Delay)
        {
            _isAttacking = false;
            _timeSinceLastAttack = 0;
            Attack();
        }
    }

    protected virtual void Attack()
    {
        _weaponHandler.Attack(_attackDir);
    }
    public void ApplyKnockback(Transform other, float power, float duration)
    {
        _rigidbody.velocity = Vector2.zero;
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
         knockbackTimer = 0;
        _state = Define.State.Knockback;
    }

    protected abstract void Init();

    protected virtual void UpdateKnockBack()
    {
        knockbackTimer += Time.deltaTime;

        if (knockbackTimer < knockbackDuration)
        {
            _rigidbody.velocity = knockback;
        }
        else
        {
            knockbackTimer = 0f;
            knockback = Vector2.zero;
            _rigidbody.velocity = Vector2.zero;

            _state = Define.State.Moving;
        }
    }

    protected virtual void UpdateDie() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateJump() { }
    protected virtual void UpdateSkill() { }
}
