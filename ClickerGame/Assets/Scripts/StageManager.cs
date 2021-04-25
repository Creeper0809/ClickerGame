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
    int stage = 1;
    private void Start()
    {
        IngameUIManager.Instance.UpdateStage(stageText, stage);
    }
    public void Clear()
    {
        stage++;
        StartCoroutine(FadeInDadeOut());
    }
    IEnumerator FadeInDadeOut()
    {
        Constants.isPaused = true;
        fade.FadeIn(1f);
        yield return new WaitForSeconds(1f);
        fade.FadeOut(1f);
        createEnemy();
        IngameUIManager.Instance.UpdateStage(stageText, stage);
        Constants.isPaused = false;
        player.transform.position = playerSpawnPos.transform.localPosition;
    }
    public void createEnemy()
    {
        Enemy.SetActive(true);
        Enemy.GetComponent<Enemy>().ResetStat();
    }
}
