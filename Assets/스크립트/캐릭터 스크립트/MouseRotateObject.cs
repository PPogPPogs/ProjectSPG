using UnityEngine;

public class MouseRotateObject : MonoBehaviour
{
    private bool isAiming = false;
    private Vector3 initialMousePosition;
    private PlayerController playerController; // PlayerController ��ũ��Ʈ ���� ����
    private Quaternion initialRotation;
    private float minAngle = -45f; // �ּ� ���� (flipX�� true�� ��)
    private float maxAngle = 45f; // �ִ� ���� (flipX�� true�� ��)
    private float minAngleFlipX = 135f; // �ּ� ���� (flipX�� false�� ��)
    private float maxAngleFlipX = 225f; // �ִ� ���� (flipX�� false�� ��)

    private void Start()
    {
        // PlayerController ��ũ��Ʈ�� ���� ������Ʈ�� ã�� �����մϴ�.
        playerController = FindObjectOfType<PlayerController>();
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAiming = true;
            initialMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isAiming = false;
            transform.rotation = initialRotation;
        }

        if (isAiming)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle;

            if (playerController.GetFlipX())
            {
                // flipX�� true�� ���� minAngle���� maxAngle ������ ����
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle = Mathf.Clamp(angle, minAngle, maxAngle);
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                // flipX�� false�� ���� minAngleFlipX���� maxAngleFlipX ������ ����
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                if (angle < 0) angle += 360f; // ���� ������ ��� ������ ��ȯ
                angle = Mathf.Clamp(angle, 135f, 225f);

                transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
            }
        }
    }
}
