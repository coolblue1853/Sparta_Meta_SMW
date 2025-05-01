using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : StatHandler
{
    // 점프 관련 능력치

    [Range(0, 10f)][SerializeField] private float jumpPower = 5f;
    public float JumpPower
    {
        get => jumpPower;
        set => jumpPower = Mathf.Clamp(value, 0, 10);
    }
    [Range(0, 30f)][SerializeField] private float gravity = 20f;
    public float Gravity
    {
        get => gravity;
        set => gravity = Mathf.Clamp(value, 0, 30);
    }
}
