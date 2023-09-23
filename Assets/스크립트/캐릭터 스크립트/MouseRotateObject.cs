using UnityEngine;

public class MouseRotateObject : MonoBehaviour
{
    private bool isAiming = false;
    private Vector3 initialMousePosition;
    private PlayerController playerController; // PlayerController 스크립트 참조 변수
    private Quaternion initialRotation;
    private float minAngle = -45f; // 최소 각도 (flipX가 true일 때)
    private float maxAngle = 45f; // 최대 각도 (flipX가 true일 때)
    private float minAngleFlipX = 135f; // 최소 각도 (flipX가 false일 때)
    private float maxAngleFlipX = 225f; // 최대 각도 (flipX가 false일 때)

    private void Start()
    {
        // PlayerController 스크립트를 가진 오브젝트를 찾아 참조합니다.
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
                // flipX가 true일 때는 minAngle에서 maxAngle 범위로 제한
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle = Mathf.Clamp(angle, minAngle, maxAngle);
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                // flipX가 false일 때는 minAngleFlipX에서 maxAngleFlipX 범위로 제한
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                if (angle < 0) angle += 360f; // 음수 각도를 양수 각도로 변환
                angle = Mathf.Clamp(angle, 135f, 225f);

                transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
            }
        }
    }
}
