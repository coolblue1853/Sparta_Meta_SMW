using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float _delay = 1f;
    public float Delay { get => _delay; set => _delay = value; }

    [SerializeField] private float _weaponSize = 1f;
    public float WeaponSize { get => _weaponSize; set => _weaponSize = value; }

    [SerializeField] private float _power = 1f;
    public float Power { get => _power; set => _power = value; }

    [SerializeField] private float _speed = 1f;
    public float Speed { get => _speed; set => _speed = value; }

    [SerializeField] private float _attackRange = 10f;
    public float AttackRange { get => _attackRange; set => _attackRange = value; }

    public LayerMask target;

    [Header("Knock Back Info")]
    [SerializeField] private bool _isOnKnockback = false;
    public bool IsOnKnockback { get => _isOnKnockback; set => _isOnKnockback = value; }

    [SerializeField] private float _knockbackPower = 0.1f;
    public float KnockbackPower { get => _knockbackPower; set => _knockbackPower = value; }

    [SerializeField] private float _knockbackTime = 0.5f;
    public float KnockbackTime { get => _knockbackTime; set => _knockbackTime = value; }

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");


    public BaseController Controller { get; private set; }
    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();
        transform.localScale = Vector3.one * _weaponSize;
    }
    public virtual void Attack(Vector2 attackDir)
    {
        AttackAnimation();
    }

    public void AttackAnimation()
    {
    //    animator.SetTrigger(IsAttack);
    }

    public virtual void Rotate(bool isLeft)
    {
    //    weaponRenderer.flipY = isLeft;
    }
}
