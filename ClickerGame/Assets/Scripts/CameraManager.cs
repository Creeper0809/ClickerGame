using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(Camera))]
public class CameraManager : Singleton<CameraManager>
{
    public GameObject player;
    public float Speed;
    public float xPlayerDistance;
    Vector3 targetPos;
    public BoxCollider2D bound;
    private Vector3 minBound;
    private Vector3 maxBound;

    // 카메라의 반넓이와 반높이의 값 변수
    private float halfWidth;
    private float halfHeight;

    // 반 높이를 구하기 위해 필요한 카메라 변수
    private Camera theCamera;
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        rect.width = 1;
        rect.height = 1;
        camera.rect = rect;
        Screen.SetResolution(1280, 720, true);
        Screen.fullScreen = true;
        DisableSystemUI.DisableNavUI();
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        Debug.Log(Camera.main.WorldToScreenPoint(transform.position));
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

    }
    private void Update()
    {
        CameraMovement();
    }
    public void CameraMovement()
    {
        if (player.gameObject != null)
        {
            // this는 카메라를 의미 (z값은 카메라값을 그대로 유지)
            targetPos.Set(player.transform.position.x + xPlayerDistance, 0, this.transform.position.z);

            // vectorA -> B까지 T의 속도로 이동
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * Speed);

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            this.transform.position = new Vector3(clampedX, 0, this.transform.position.z);
        }
    }
}
