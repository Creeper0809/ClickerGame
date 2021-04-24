using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
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
    void SearchEnemy()
    {
        Collider[] t_col = Physics.OverlapSphere(transform.position, range, layerMask);
        Transform shortestTarget = null;
        if (t_col.Length > 0)
        {
            float shortestDistance = Mathf.Infinity;
            foreach(Collider colTarget in t_col)
            {
                float distance = Vector3.SqrMagnitude(transform.position - colTarget.transform.position);
                if (shortestDistance > distance)
                {
                    shortestDistance = distance;
                    shortestTarget = colTarget.transform;
                }
            }
        }
        target = shortestTarget;
    }
    private void Start()
    {
        boxCollider = GetComponent<CircleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (firerate > timer)
        {
            timer += Time.deltaTime;
            return;
        }
        Fire();
        if (isInRange)
        {
            
            return;
        }
/*        Vector3 vector = Vector3.right;
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
        transform.Translate(vector * velocity * Time.fixedDeltaTime);*/
        /*        if (target != null)
                {
                    Debug.Log(target.name);
                }*/
    }
    public void Fire()
    {
        if (firerate > timer)
        {
            return;
        }
        GameObject obj = createBullet("Bullet", transform);
        obj.transform.position = transform.localPosition;
       /* ObjectPoolManager.getInstance().pool.Pop().transform.position = shootPos.transform.position;*/
        timer = 0.0f;
    }
    private GameObject createBullet(string name,Transform parentNode)
    {
        GameObject obj = ObjectPoolManager.Instance.Spawn(name, parentNode);
        if (obj == null) //아직 오브젝트풀에 생성된 적이 없는 오브젝트 
        {
            GameObject prefab = (GameObject)Resources.Load(name); //이 오브젝트는 처음 로드하므로 Resources.Load()로 읽는다. 
            if (prefab == null)
            {
                Debug.Log("Failed Load Prefab : " + name);
                return null;
            } //읽어온 프리팹의 오브젝트풀을 생성한다. //오브젝트 20개를 미리 생성해 두며 20개가 다 소진되면 5개씩 추가 될 것이다. 
            if (ObjectPoolManager.Instance.InitializeSpawn(prefab, 5, 20))
            { //생성된 풀에서 오브젝트 한개를 꺼내온다 
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
}
