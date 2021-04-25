using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bullet")
        {
            ObjectPoolManager.Instance.Despawn(other.transform.gameObject);
            TakeDamage(10000);
        }
    }

}
