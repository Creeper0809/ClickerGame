using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public override void ResetStat()
    {
        HP = 100000f;
        MaxHP = 100000f;
        TakeDamage(0);
    }

    private void Start()
    {
        HPsl = GameObject.Find("EnemyHPBar").gameObject;
        HPtext = HPsl.transform.GetChild(4).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
