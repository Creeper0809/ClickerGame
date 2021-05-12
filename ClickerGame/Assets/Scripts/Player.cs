using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : Character
{
    public GameObject HpUIText;
    public TMP_Text manaText;
    private static Player instance;
    public Vector3 playerPos;
    protected float manaTimer;
    protected float manaRegeneration = 2f;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static Player Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public Player()
    {
        MaxMp = 10;
        MaxHp = 1000;
        velocity = 3f;
        firerate = 0.3f;
        range = 2f;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        manaText.text = NowMp.ToString();
        playerPos = gameObject.transform.position;
        HpUIText.GetComponent<Text>().text = "HP:" + NowHp + "/" + MaxHp;
        fireRateTimer += Time.deltaTime;
        if (manaRegeneration < manaTimer && NowMp <MaxMp)
        {
            NowMp++;
            manaTimer = 0;
        }
        else if(NowMp == MaxMp)
        {
            manaTimer = 0;
        }
        else
        {
            manaTimer += Time.deltaTime;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, LayerMask.GetMask("Enemy"));
        if (hit)
        {
            Fire();
            return;
        }
        Vector3 vector = Vector3.right;
        transform.Translate(vector * velocity * Time.deltaTime);
    }
    public void UseMana(int howMany)
    {
        NowMp -= howMany;
    }
    public void Fire()
    {
        if (firerate > fireRateTimer)
        {
            return;
        }
        GameObject obj = ObjectPoolManager.Instance.getPool("Bullet", transform);
        obj.transform.rotation = Quaternion.Euler(0, 0, 270);
        obj.transform.position = this.transform.position;
        obj.transform.localScale = new Vector3(1, 1, 1);
        fireRateTimer = 0.0f;
    }
    public override void die()
    {
        gameObject.SetActive(false);
        Constants.isPaused = true;
    }
    public float getNowMp()
    {
        return NowMp;
    }
}
