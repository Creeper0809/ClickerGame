using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    public float MaxHp;
    protected float NowHp;
    public float firerate;
    public float velocity;
    public float range;
    public float MaxMp;
    protected float NowMp;
    protected bool isVisible = false;
    protected BoxCollider2D boxCollider;
    private Transform canvas;
    protected float fireRateTimer;
    protected float hpBarHeight = -0.5f;
    public GameObject HpBar;
    public GameObject MpBar;
    public GameObject HpText;
    public GameObject MpText;
    protected TypeOfCharacter typeOfCharacter;
    public enum TypeOfCharacter
    {
        PLAYER,
        ENEMY,
        BOSS
    };

    protected virtual void Start()
    {
        canvas = GameObject.Find("HPBarCanvas").transform;
        ResetStat();
    }
    protected virtual void Update()
    {
        SetHPBar();
    }
    public void SetHPBar()
    {
        if(HpBar == null)
        {
            HpBar = ObjectPoolManager.Instance.getPool("HPBar", canvas);
            HpBar.transform.SetParent(canvas);
            HpText = HpBar.transform.GetChild(3).gameObject;
        }
        HpBar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + hpBarHeight, 0));
        IngameManager.Instance.UpdateMpHp(NowHp, MaxHp, HpBar, HpText);
    }
    public virtual void ResetStat()
    {
        NowMp = MaxMp;
        NowHp = MaxHp;
    }
    public virtual void TakeDamage(int damage)
    {
        NowHp = Mathf.Clamp(NowHp - damage, 0, MaxHp);
        if (NowHp == 0)
        {
            die();
        }
        if (HpBar == null) return;
        IngameManager.Instance.UpdateMpHp(NowHp, MaxHp, HpBar, HpText);
    }
    public abstract void die();
}
