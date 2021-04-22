using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject shootPos;
    public float firerate = 1f;
    public float timer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
            if(firerate > timer)
            {
                timer += Time.deltaTime;
            }
            
        }
    }
    void Fire()
    {
        if (firerate > timer)
        {
            return;
        }
        ObjectPoolManager.getInstance().pool.Pop().transform.position = shootPos.transform.position;
        timer = 0.0f;
    }
}
