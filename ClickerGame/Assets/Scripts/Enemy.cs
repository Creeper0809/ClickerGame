using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject shootPos;
    public bool isClicked = false;
    public float firerate = 1f;
    public float timer;
    public bool isInRange = false;
    public float velocity = 0.1f;
    // Update is called once per frame
    void Update()
    {
        if (firerate > timer)
        {
            timer += Time.deltaTime;
            return;
        }
        if (isInRange)
        {
            Fire();
        }

    }
    public void Fire()
    {
        if (firerate > timer)
        {
            return;
        }
        /*ObjectPoolManager.getInstance().pool.Pop().transform.position = shootPos.transform.position;*/
        timer = 0.0f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isInRange = true;
        }
        if (other.tag == "bullet")
        {
            ObjectPoolManager.Instance.Despawn(other.transform.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isInRange = false;
        }
    }
}
