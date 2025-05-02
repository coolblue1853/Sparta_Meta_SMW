using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] protected LayerMask levelCollisionLayer;

    protected RangeWeaponHandler rangeWeaponHandler;

    protected float currentDuration;
    protected Vector2 direction;
    protected bool isReady;
    protected Transform pivot;

    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer spriteRenderer;

    public bool fxOnDestory = true;
    [SerializeField] protected bool isDestroyByCollsion;

    protected void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
    }

    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        rangeWeaponHandler = weaponHandler;

        this.direction = direction;
        currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
       // spriteRenderer.color = weaponHandler.ProjectileColor;

        transform.right = this.direction;

   
        isReady = true;
    }

    protected void DestroyProjectile(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject);
    }
}
