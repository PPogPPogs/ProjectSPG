using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 5.0f; // 줌 속도
    public float minFOV = 20.0f; // 최소 FOV
    public float maxFOV = 60.0f; // 최대 FOV

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 특정 키를 누르면 FOV를 변경하여 카메라를 줌인 또는 줌아웃합니다.
        float fov = mainCamera.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        fov = Mathf.Clamp(fov, minFOV, maxFOV);
        mainCamera.fieldOfView = fov;
    }
}
