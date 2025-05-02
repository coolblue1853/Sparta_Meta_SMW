using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultHealth : Result
{
    [SerializeField] private float healHealth = 20;
    protected override void GetResult(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            resourceController.UpdateHealth(healHealth);
        }

    }
}
