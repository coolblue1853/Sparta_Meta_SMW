using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPrejectileCount : Result
{
    protected override void GetResult(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            RangeWeaponHandler rangeWeaponHandler = playerController._weaponHandler.GetComponent<RangeWeaponHandler>();
            rangeWeaponHandler.NumberofProjectilesPerShot += 1;
        }
    }
}
