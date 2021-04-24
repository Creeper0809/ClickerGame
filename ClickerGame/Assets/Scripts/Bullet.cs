using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float power = 1000f;
    Rigidbody2D rb2d;
    Vector3 dir;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        dir = transform.right;
    }
    private void OnEnable()
    {
        rb2d.AddForce(dir * power);
    }
    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
        {
            ObjectPoolManager.Instance.Despawn(transform.gameObject);
        }
    }
}
