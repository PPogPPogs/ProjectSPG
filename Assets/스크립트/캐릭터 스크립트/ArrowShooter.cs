using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // 화살 프리팹을 할당하세요.
    public Transform arrowSpawnPoint; // 화살이 발사될 위치를 할당하세요.
    public float arrowSpeed = 10.0f;

    private bool isAiming = false;

    private void Update()
    {
        // 마우스 우클릭 감지
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
        }

        // 마우스 우클릭 상태에서 좌클릭으로 화살 발사
        if (isAiming && Input.GetMouseButtonDown(0))
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        // 화살을 활성화시키고 초기 위치와 회전을 설정합니다.
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

        // 커서 위치를 월드 좌표로 변환하여 방향 벡터 계산
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (cursorPosition - (Vector2)arrowSpawnPoint.position).normalized;

        // 화살에 방향과 속도를 설정하여 발사합니다.
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * arrowSpeed;
        }
    }
}
