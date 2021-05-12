using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public FadeInControllor fade;
    public GameObject playerSpawnPos;
    public GameObject player;

    public GameObject stageText;

    public GameObject Enemy;
    public GameObject EnemySpawnPos;

    public GameObject MobParent;
    public List<GameObject> mobs;
    public int trashMobCount = 10;
    float timer = 0f;
    protected StageManager() { }
    private void Start()
    {
        Constants.stage = PlayerPrefs.GetInt("stage");
        if (Constants.stage == 0)
        {
            Constants.stage++;
        }
        IngameManager.Instance.UpdateStage(stageText, Constants.stage);
        MobParent = GameObject.Find("TrashMobs");
        CreateMob(20);
    }
    public void CreateMob(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = ObjectPoolManager.Instance.getPool("TrashMob", MobParent.transform);
            mobs.Add(obj);
            obj.GetComponent<TrashMob>().Player = GameObject.FindGameObjectWithTag("Player");
            obj.GetComponent<TrashMob>().velocity = Random.RandomRange(1f, 3f);
            float objY = Random.RandomRange(-0.45f, -0.1f);
            obj.transform.position = new Vector3(EnemySpawnPos.transform.position.x, objY, 0);
        }
    }
    public void Clear()
    {
        Constants.stage++;
        Constants.isPaused = true;
        Debug.Log(Constants.stage);
        foreach (GameObject iter in mobs)
        {
            ObjectPoolManager.Instance.Despawn(iter);
        }
        StartCoroutine(FadeInDadeOut());
    }
    IEnumerator FadeInDadeOut()
    {
        fade.FadeIn(2f);
        yield return new WaitForSeconds(1f);
        fade.FadeOut(2f);
        CreateMob(Random.RandomRange(10,20));
        IngameManager.Instance.UpdateStage(stageText, Constants.stage);
        player.transform.position = playerSpawnPos.transform.localPosition;
        Constants.isPaused = false;
        Constants.isCleared = false;
    }
}
