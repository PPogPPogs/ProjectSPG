using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 5.0f; // �� �ӵ�
    public float minFOV = 20.0f; // �ּ� FOV
    public float maxFOV = 60.0f; // �ִ� FOV

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Ư�� Ű�� ������ FOV�� �����Ͽ� ī�޶� ���� �Ǵ� �ܾƿ��մϴ�.
        float fov = mainCamera.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        fov = Mathf.Clamp(fov, minFOV, maxFOV);
        mainCamera.fieldOfView = fov;
    }
}
