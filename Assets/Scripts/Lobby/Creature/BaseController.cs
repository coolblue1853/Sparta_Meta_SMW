using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    //자식의 스프라이트 오브젝트
    [SerializeField]
    protected SpriteRenderer _spriteRenderer;
    protected Rigidbody2D _rigidbody;

    [SerializeField]
    protected Define.State _state = Define.State.Idle;
    [SerializeField]
    protected Define.Dir _animDir = Define.Dir.None;
    [SerializeField]
    protected Vector2 _moveDir = Vector2.zero;
    public Vector2 _MoveDir { get { return _moveDir; } }

    // 이동 관련 변수
    [SerializeField]
    protected float _movePower = 0f;
    // 점프 관련 변수
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
    }

    private void FixedUpdate()
    {
        switch (State)
        {
            case Define.State.Moving:
                UpdateMoving();
                break;
        }

        // 점프 관련연산
        UpdateJump();
    }

    protected abstract void Init();

   // protected virtual void UpdateDie() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateJump() { }
  //  protected virtual void UpdateSkill() { }
}
