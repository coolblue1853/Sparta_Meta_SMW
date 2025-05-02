using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Result : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetResult(collision);
            Destroy(this.gameObject);
        }  
    }

    protected abstract void GetResult(Collider2D collision);

}
