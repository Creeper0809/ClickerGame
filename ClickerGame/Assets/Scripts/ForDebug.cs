using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForDebug : MonoBehaviour
{
    public GameObject player;
    public GameObject boss;
    private void Start()
    {
        player = GameObject.Find("Player");
        boss = GameObject.Find("BOSS");
    }
    public void ResetGameAll()
    {
        Constants.stage = 0;
        Constants.money = 0;
        player.GetComponent<Character>().ResetStat();
        player.SetActive(true);
        boss.GetComponent<Character>().ResetStat();
        StageManager.Instance.Clear();
        IngameManager.Instance.UpdateMoney();
    }
    public void EarnMoney()
    {
        Constants.money += 1000;
        IngameManager.Instance.UpdateMoney();
    }
    public void LossMoney()
    {
        Constants.money -= 1000;
        IngameManager.Instance.UpdateMoney();
    }
}
