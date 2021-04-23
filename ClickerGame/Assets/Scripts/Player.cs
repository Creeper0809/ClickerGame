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
    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            Fire();
        }
        if (firerate > timer)
        {
            timer += Time.deltaTime;
        }

    }
    public void Fire()
    {
        if (firerate > timer)
        {
            return;
        }
        ObjectPoolManager.getInstance().pool.Pop().transform.position = shootPos.transform.position;
        timer = 0.0f;
    }
    public void OnButtonDown()
    {
        isClicked = true;
    }
    public void OnButtonUp()
    {
        isClicked = false;
    }
}
