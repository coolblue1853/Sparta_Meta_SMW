using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : StatHandler
{
    // 점프 관련 능력치

    [Range(0, 10f)][SerializeField] private float _jumpPower = 5f;
    public float JumpPower
    {
        get => _jumpPower;
        set => _jumpPower = Mathf.Clamp(value, 0, 10);
    }
    [Range(0, 30f)][SerializeField] private float _gravity = 20f;
    public float Gravity
    {
        get => _gravity;
        set => _gravity = Mathf.Clamp(value, 0, 30);
    }
}
