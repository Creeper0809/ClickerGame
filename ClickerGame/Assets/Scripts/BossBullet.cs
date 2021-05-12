using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    float speed = 1000f;
    protected int penetration = 1;
    protected int nowPenetration = 1;
    Rigidbody2D rb2d;
    [SerializeField]
    Vector3 dir;
    public float rotatingSpeed = 200;
    public Collider2D collidersD;
    public GameObject target;
    Vector2 point2Target;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collidersD = GetComponent<BoxCollider2D>();
        target = GameObject.Find("Player").gameObject;
        Transform parent = GameObject.Find("BOSS").gameObject.transform;
        dir = (target.transform.position-parent.position ).normalized;
    }
    private void OnEnable()
    {
        rb2d.AddForce(dir * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().TakeDamage(50);
            ObjectPoolManager.Instance.Despawn(transform.gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
        {
            nowPenetration = penetration;
            ObjectPoolManager.Instance.Despawn(transform.gameObject);
        }
    }
}

