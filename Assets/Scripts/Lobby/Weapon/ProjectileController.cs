using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] protected LayerMask _levelCollisionLayer;

    protected RangeWeaponHandler _rangeWeaponHandler;

    protected float _currentDuration;
    protected Vector2 _direction;
    protected bool _isReady;
    protected Transform _pivot;

    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;

    public bool _fxOnDestory = true;
    [SerializeField] protected bool isDestroyByCollsion;

    protected void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _currentDuration += Time.deltaTime;

        if (_currentDuration > _rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = _direction * _rangeWeaponHandler.Speed;
    }

    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rangeWeaponHandler = weaponHandler;

        this._direction = direction;
        _currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;

        transform.right = this._direction;
        _isReady = true;
    }

    protected void DestroyProjectile(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject);
    }
}
