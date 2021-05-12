using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameManager : Singleton<IngameManager>
{
    public GameObject moneyText;

    public ItemDB ItemDB;
    private void Start()
    {
        moneyText = GameObject.Find("Money").gameObject;
        ItemDB = GameObject.Find("GameManager").GetComponent<ItemDB>();
        Constants.money = PlayerPrefs.GetInt("money");
        UpdateMoney();
    }
    protected IngameManager() { }
    public void UpdateMpHp(float value, float maxValue, GameObject slider, GameObject text) // 피격시 실행
    {
        float CurHP = ((float)value / (float)maxValue);
        slider.GetComponent<Slider>().value = CurHP;
        UpdateMpHp(value, maxValue, text);
    }
    public void UpdateMpHp(float value,float maxValue,GameObject text)
    {
        text.GetComponent<Text>().text = value + "/" + maxValue;
    }
    public void UpdateMoney(int money)
    {
        Constants.money += money;
        Constants.money = (int)Mathf.Clamp(Constants.money, 0, Mathf.Infinity);
        moneyText.GetComponent<Text>().text = "돈:" + Constants.money + "원";
    }

    public void UpdateMoney()
    {
        UpdateMoney(0);
    }
    public void UpdateCardDeckCount(GameObject deck,GameObject graveYard,int deckAmount,int graveAmount)
    {
        deck.GetComponent<Text>().text = deckAmount.ToString();
        graveYard.GetComponent<Text>().text = graveAmount.ToString();
    }
    public void UpdateStage(GameObject stageText, int stage)
    {
        stageText.GetComponent<Text>().text = "Stage: " + stage;
    }
}
