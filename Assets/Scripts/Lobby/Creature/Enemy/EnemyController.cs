using UnityEngine;

public class EnemyController : BaseController
{
    EnemyStatHandler _statHandler;
    private Transform target;

    [SerializeField] private float followRange = 1f;
    protected override void Init()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _statHandler = GetComponent<EnemyStatHandler>();
        this.target = LobbyScene.Instance._playerPrefab.transform;
        _state = Define.State.Moving;
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
            _rigidbody.velocity = direction * _statHandler.Speed;
        }

 
    }

    protected override void UpdateSkill()
    {
        float distance = DistanceToTarget();
        if (distance <= followRange)
        {
            _attackDir = DirectionToTarget();  // 공격 방향 설정
            _isAttacking = true;               // 공격 요청 (딜레이 체크는 HandleAttackDelay에서 함)
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            _isAttacking = false;
            _state = Define.State.Moving;
        }
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
