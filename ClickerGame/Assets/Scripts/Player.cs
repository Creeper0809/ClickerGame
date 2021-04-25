using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : Character
{
    public GameObject shootPos;
    public bool isClicked = false;
    public float firerate = 1f;
    public float timer;
    public bool isInRange = false;
    public float velocity = 0.1f;
    
    Transform me = null;
    float range = 100f;
    LayerMask layerMask;
    private CircleCollider2D boxCollider;
    Transform target = null;
    private void Start()
    {
        boxCollider = GetComponent<CircleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Constants.isPaused == true) return;
        if (firerate > timer)
        {
            timer += Time.deltaTime;
            return;
        }
        if (isInRange)
        {
            Fire();
            return;
        }
        Vector3 vector = Vector3.right;
        RaycastHit2D hit;
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(vector.x, vector.y);
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;
        if (hit.transform != null)
        {
            return;
        }
        transform.Translate(vector * velocity * Time.deltaTime);
    }
    public void Fire()
    {
        if (firerate > timer)
        {
            return;
        }
        GameObject obj = createBullet("Bullet", transform);
        obj.transform.rotation = Quaternion.Euler(0, 0, 270);
        obj.transform.position = shootPos.transform.position;
        obj.transform.localScale = new Vector3(1, 1, 1);
        timer = 0.0f;
    }
    private GameObject createBullet(string name,Transform parentNode)
    {
        GameObject obj = ObjectPoolManager.Instance.Spawn(name, parentNode);
        if (obj == null) 
        {
            GameObject prefab = (GameObject)Resources.Load(name);
            if (prefab == null)
            {
                Debug.Log("Failed Load Prefab : " + name);
                return null;
            }
            if (ObjectPoolManager.Instance.InitializeSpawn(prefab, 5, 20))
            { 
                obj = ObjectPoolManager.Instance.Spawn(prefab.name, parentNode);
            }
        }
        return obj;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            isInRange = false;
        }
    }

    public override void ResetStat()
    {
        HP = 1000f;
        MaxHP = 1000f;
        TakeDamage(0);
    }
}
