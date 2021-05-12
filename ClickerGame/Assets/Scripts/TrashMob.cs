using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMob : Mobs
{
    public TrashMob()
    {
        NowHp = 100;
        MaxHp = 100;
        deadMoney = 100;
        velocity = 4.5f;
        range = 2f;
        firerate = 1;
        typeOfCharacter = TypeOfCharacter.ENEMY;
    }
    private void Update()
    {
        base.Update();
        gracePeriodTimer += Time.deltaTime;
        if (firerate > fireRateTimer)
        {
            fireRateTimer += Time.deltaTime;
            return;
        }
        if (Vector2.Distance(transform.position, Player.transform.position) >= range)
        {
            Vector3 vector = Vector3.left;
            transform.Translate(vector * velocity * Time.deltaTime);
        }
        else
        {
            if (firerate > fireRateTimer)
            {
                return;
            }
            Player.GetComponent<Character>().TakeDamage(1);
            fireRateTimer = 0;
        }
    }
    protected override void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        base.Start();
    }
    public override void die()
    {
        ObjectPoolManager.Instance.Despawn(HpBar);
        HpBar = null;
        ObjectPoolManager.Instance.Despawn(transform.gameObject);
        StageManager.Instance.mobs.Remove(gameObject);
        IngameManager.Instance.UpdateMoney(deadMoney);
        ResetStat();
    }
    public override void TakeDamage(int damage)
    {
        if(gracePeriod > gracePeriodTimer)
        {
            return;
        }
        gracePeriodTimer = 0f;
        base.TakeDamage(damage);
    }
}
