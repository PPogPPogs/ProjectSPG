using UnityEngine;

public class MouseRotateObject : MonoBehaviour
{
    private bool isAiming = false;
    private Vector3 lastMousePosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            // 조준이 끝나면 초기 회전으로 돌아갑니다.
            transform.rotation = initialRotation;
        }

        if (isAiming)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle , Vector3.forward);
            transform.rotation = rotation;
        }
    }


}

