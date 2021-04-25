using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIManager : Singleton<IngameUIManager>
{
    public void UpdateHp(float HP,float MaxHP,GameObject slHP,GameObject HPText) // 피격시 실행
    {
        float CurHP = ((float)HP / (float)MaxHP);
        slHP.GetComponent<Slider>().value = CurHP;
        Debug.Log(slHP.name + "," + HP + "," + MaxHP + "," + CurHP+","+ slHP.GetComponent<Slider>().value);
        if (HP < 0)
        {
            HP = 0;
        }
        HPText.GetComponent<Text>().text = HP + "/" + MaxHP;

    }
    public void UpdateStage(GameObject stageText,int stage)
    {
        stageText.GetComponent<Text>().text = "Stage: " + stage;
    }
}
