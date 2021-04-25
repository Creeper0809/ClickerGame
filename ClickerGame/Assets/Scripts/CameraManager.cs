using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        rect.width = 1;
        rect.height = 1;
        camera.rect = rect;
        Screen.SetResolution(720, 1280, true);
        Screen.fullScreen = true;
        DisableSystemUI.DisableNavUI();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
