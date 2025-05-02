using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : StatHandler
{
    [Range(0, 30f)][SerializeField] private float _gravity = 20f;
    public float Gravity
    {
        get => _gravity;
        set => _gravity = Mathf.Clamp(value, 0, 30);
    }
}
