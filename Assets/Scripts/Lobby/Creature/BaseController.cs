using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    //�ڽ��� ��������Ʈ ������Ʈ
    [SerializeField]
    protected SpriteRenderer _spriteRenderer;
    protected Rigidbody2D _rigidbody;

    [Header("Define")]
    [SerializeField]
    protected Define.State _state = Define.State.Idle;
    [SerializeField]
    protected Define.Dir _animDir = Define.Dir.None;

    [Header("Dir")]
    [SerializeField]
    protected Vector2 _moveDir = Vector2.zero;
    public Vector2 _MoveDir { get { return _moveDir; } }


    [Header("Weapon Info")]
    [SerializeField] public WeaponHandler _WeaponPrefab;
    [SerializeField] protected WeaponHandler _weaponHandler;
    [SerializeField]
    private Transform _weaponPivot;
    protected Vector2 _attackDir = Vector2.right;
    protected bool _isAttacking;
    private float _timeSinceLastAttack = float.MaxValue;
    protected bool _isCanAttack = true;

    [Header("Stat Info")]
    // �̵� ���� ���� - ���� �������� �Ѱܾ���
    [SerializeField]
    protected float _movePower = 0f;
    // ���� ���� ���� - ���� �������� �Ѱܾ���
    protected float _height = 0f;
    [SerializeField]
    protected float _jumpPower = 0f;
    protected float _verticalSpeed = 0f;
    [SerializeField]
    protected float _gravity = 0f;
    protected bool _isGrounded = true;


    private void Awake()
    {
        Init();

        if (_WeaponPrefab != null)
        {
            _weaponHandler = Instantiate(_WeaponPrefab, this.transform);
            _weaponHandler.transform.position = _weaponPivot.position;
        }


    }

    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Die:
                    break;
                case Define.State.Idle:
                  //  anim.CrossFade("Wait", 0.1f);
                    break;
                case Define.State.Moving:
                    //anim.CrossFade("Run", 0.1f);
                    break;
                case Define.State.Skill:
                  //  anim.CrossFade("Attack", 0.1f, -1, 0);
                    break;
            }
        }
    }

    void Update()
    {
        switch (State)
        {
            case Define.State.Die:
               // UpdateDie();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Skill:
              //  UpdateSkill();
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
        }

        // ���� ���ÿ���
        UpdateJump();
    }


    private void HandleAttackDelay()
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
    protected abstract void Init();

   // protected virtual void UpdateDie() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateJump() { }
  //  protected virtual void UpdateSkill() { }
}
