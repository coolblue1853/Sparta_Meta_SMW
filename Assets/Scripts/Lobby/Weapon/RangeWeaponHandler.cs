using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEngine.UI.Image;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform _projectileSpawnPosition;

    [SerializeField] private int _bulletIndex;
    public int BulletIndex { get { return _bulletIndex; } }

    [SerializeField] private float _bulletSize = 1;
    public float BulletSize { get { return _bulletSize; } }

    [SerializeField] private float _duration;
    public float Duration { get { return _duration; } }

    [SerializeField] private float _spread;
    public float Spread { get { return _spread; } }

    [SerializeField] private int _numberofProjectilesPerShot;
    public int NumberofProjectilesPerShot { set { _numberofProjectilesPerShot = value;} get { return _numberofProjectilesPerShot; } }

    [SerializeField] private float _multipleProjectilesAngel;
    public float MultipleProjectilesAngel { get { return _multipleProjectilesAngel; } }

    [SerializeField] private Color _projectileColor;
    public Color ProjectileColor { get { return _projectileColor; } }

    [SerializeField] ProjectileController _ProjectileObject;

    public override void Attack(Vector2 attackDir)
    {
        base.Attack(attackDir);

        float projectilesAngleSpace = _multipleProjectilesAngel;
        int numberOfProjectilesPerShot = _numberofProjectilesPerShot;

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace;

        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-_spread, _spread);
            angle += randomSpread;
            CreateProjectile(attackDir, angle);
        }
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        // angle을 적용한 방향으로 회전
        Vector2 rotatedDirection = RotateVector2(_lookDirection.normalized, angle);

        GameObject obj = Instantiate(_ProjectileObject.gameObject, _projectileSpawnPosition.position, Quaternion.identity);
        obj.GetComponent<ProjectileController>().Init(rotatedDirection, this);
    }
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
