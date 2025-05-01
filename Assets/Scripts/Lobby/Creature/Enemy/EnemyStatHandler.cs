using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : StatHandler
{
    [Range(0, 30f)][SerializeField] private float gravity = 20f;
    public float Gravity
    {
        get => gravity;
        set => gravity = Mathf.Clamp(value, 0, 30);
    }
}
