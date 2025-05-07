using UnityEngine;

public class EnemyController : BaseController
{
    private RoundManager _roundManager;
    EnemyStatHandler _statHandler;
    private Transform target;

    [SerializeField] private WeaponHandler _enemyWeaponHandler;
    [SerializeField] private float followRange = 1f;
    protected override void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _statHandler = GetComponent<EnemyStatHandler>();
        this.target = LobbyScene.Instance._playerPrefab.transform;
        _state = Define.State.Moving;
        CreatWeapon(_enemyWeaponHandler);
    }
    public void SetRoundManager(RoundManager roundManager)
    {
        _roundManager = roundManager;
    }

    protected override void UpdateMoving()
    {
        base.UpdateMoving();
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        if (distance <= followRange) // 근접했다면 공격하기
        {
            _state = Define.State.Skill;
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            ChangeDir(direction);
            _rigidbody.velocity = direction * _statHandler.Speed;
        }
    }

    protected override void UpdateSkill()
    {
        float distance = DistanceToTarget();
        if (distance <= followRange)
        {
            _attackDir = DirectionToTarget();

            ChangeDir(_attackDir);
            _isAttacking = true;               
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            _isAttacking = false;
            _state = Define.State.Moving;
        }
    }

    void ChangeDir(Vector2 input)
    {
        if (input.x > 0)
        {
            _mainSpriteRenderer.flipX = false;
            _weaponRnderer.flipX = false;
        }

        if (input.x < 0)
        {
            _mainSpriteRenderer.flipX = true;
            _weaponRnderer.flipX = true;
        }
    }

    protected override void UpdateDie()
    {
        base.UpdateDie();
        _roundManager?.RemoveEnemy(this);
        //가능하다면 오브젝트 풀링 적용
        Destroy(gameObject);
    }
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }
}
