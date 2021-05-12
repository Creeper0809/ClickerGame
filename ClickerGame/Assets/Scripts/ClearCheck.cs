using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCheck : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!Constants.isCleared)
            {
                Debug.Log("플레이어 닿음");
                StageManager.Instance.Clear();
            }
        }
    }
}
