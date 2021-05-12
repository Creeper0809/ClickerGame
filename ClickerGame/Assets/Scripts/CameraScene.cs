using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScene : MonoBehaviour
{
    public void ToMainScene()
    {
        LoadingSceneManager.LoadScene("MainScene");
    }
}
