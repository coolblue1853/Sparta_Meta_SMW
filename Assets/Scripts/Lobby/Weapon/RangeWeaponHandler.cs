using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEngine.UI.Image;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1;
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectilesAngel;
    public float MultipleProjectilesAngel { get { return multipleProjectilesAngel; } }

    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get { return projectileColor; } }

    [SerializeField] ProjectileController ProjectileObject;

    public override void Attack(Vector2 attackDir)
    {
        base.Attack(attackDir);

        float projectilesAngleSpace = multipleProjectilesAngel;
        int numberOfProjectilesPerShot = numberofProjectilesPerShot;

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace;


        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(attackDir, angle);
        }
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        GameObject obj = Instantiate(ProjectileObject.gameObject, projectileSpawnPosition.position, Quaternion.identity);
        obj.GetComponent<ProjectileController>().Init(_lookDirection, this);

    }
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
