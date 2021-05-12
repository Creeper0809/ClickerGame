using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public void UseSkill()
    {
        Debug.Log("¹ß»ç´ï");
        GameObject obj = ObjectPoolManager.Instance.getPool("SkillBullet", Player.Instance.transform);
        obj.transform.rotation = Quaternion.Euler(0, 0, 270);
        obj.transform.position = Player.Instance.playerPos;
        obj.transform.localScale = new Vector3(1, 1, 1);
    }
}
