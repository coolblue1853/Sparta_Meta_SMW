using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveController : ProjectileController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
        }
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            //npc 상호작용
            var npcController = collision.GetComponent<BaseNpc>();
            if (npcController != null)
            {
                npcController.InteractiveNPC();
            }

            if (isDestroyByCollsion)
                DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }
}
