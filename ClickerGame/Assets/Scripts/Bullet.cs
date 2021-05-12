using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float power = 1000f;
    public int penetration = 1;
    protected int nowPenetration;
    Rigidbody2D rb2d;
    Vector3 dir;
    public LayerMask Enemy;
    public float range;
    public Collider2D collidersD;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        dir = transform.right;
        collidersD = GetComponent<BoxCollider2D>();
        nowPenetration = penetration;
    }
    private void Update()
    {
        RaycastHit2D hit; //Raycast데이터 저장용 배열 설정
        hit = Physics2D.Raycast(transform.position, Vector2.right, 0.3f,LayerMask.GetMask("Enemy"));
        if (hit)
        {
            hit.collider.GetComponent<Character>().TakeDamage(50);
            nowPenetration--;
            if (nowPenetration == 0)
            {
                nowPenetration = penetration;
                ObjectPoolManager.Instance.Despawn(transform.gameObject);
            }
        }
    }
    private void OnEnable()
    {
        rb2d.AddForce(dir * power);
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
