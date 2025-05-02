using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackController : ProjectileController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (_levelCollisionLayer.value == (_levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, _fxOnDestory);
        }
        else if (_rangeWeaponHandler.target.value == (_rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-_rangeWeaponHandler.Power);
                if (_rangeWeaponHandler.IsOnKnockback)
                {
                    BaseController controller = collision.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, _rangeWeaponHandler.KnockbackPower, _rangeWeaponHandler.KnockbackTime);
                    }
                }
            }

            DestroyProjectile(collision.ClosestPoint(transform.position), _fxOnDestory);
        }
     
    }
}
