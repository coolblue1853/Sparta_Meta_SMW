using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSpeed : Result
{
    [SerializeField] private float upSpeed = 1;
    protected override void GetResult(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            resourceController.UpdateSpeend(upSpeed);
        }

    }
}
