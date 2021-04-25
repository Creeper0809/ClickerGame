using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected float MaxHP;
    protected float HP;
    public GameObject HPsl;
    public GameObject HPtext;
    private void Awake()
    {
        ResetStat();
    }
    public abstract void ResetStat();
    public void TakeDamage(int damage)
    {
        HP = Mathf.Clamp(HP-damage,0,MaxHP);
        if(HP == 0)
        {
            die();
        }
        IngameUIManager.Instance.UpdateHp(HP,MaxHP,HPsl,HPtext);
    }
    public void die()
    {
        gameObject.SetActive(false);
    }
}
