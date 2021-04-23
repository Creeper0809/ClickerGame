﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private Poolable poolObj;
    [SerializeField]
    private int allocateCount;
    [SerializeField]
    private Stack<Poolable> poolStack = new Stack<Poolable>();
    void Start()
    {
        Allocate();
    }

    public void Allocate()
    {
        for(int i = 0; i < allocateCount; i++)
        {
            Poolable allocateObj = Instantiate(poolObj,gameObject.transform);
            allocateObj.Create(this);
            poolStack.Push(allocateObj);
        }
    }
    public GameObject Pop()
    {
        Debug.Log("총알 생성");
        Poolable obj = poolStack.Pop();
        obj.gameObject.SetActive(true);
        return obj.gameObject;
    }
    public void Push(Poolable obj)
    {
        poolStack.Push(obj);
        obj.gameObject.SetActive(false);
    }
}
