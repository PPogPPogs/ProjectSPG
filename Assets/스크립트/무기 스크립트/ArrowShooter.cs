using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // 화살 프리팹을 할당하세요.
    public Transform arrowSpawnPoint; // 화살이 발사될 위치를 할당하세요.
    public float arrowSpeed = 10.0f;
    private float minAngle = -45f; // 최소 각도 (flipX가 true일 때)
    private float maxAngle = 45f; // 최대 각도 (flipX가 true일 때)
    private PlayerController playerController;
    private bool isAiming = false;

    private void Start()
    {
        // PlayerController 스크립트를 가진 오브젝트를 찾아 참조합니다.
        playerController = FindObjectOfType<PlayerController>();
        
    }

    private void Update()
    {
        // 마우스 우클릭 감지
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
        }
        else if (Input.GetMouseButtonUp(1))
            {
            isAiming = false;
        }
       
        

        // 마우스 우클릭 상태에서 좌클릭으로 화살 발사
        if (isAiming && Input.GetMouseButtonUp(0))
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

        // 현재 각도를 계산합니다.
        float angle;

        // 만약 각도가 minAngle 또는 maxAngle 범위 내에 있다면 방향을 해당 각도로 설정합니다.

        if (playerController.GetFlipX())
        {
            // flipX가 true일 때는 minAngle에서 maxAngle 범위로 제한
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, minAngle, maxAngle);
            direction = Quaternion.Euler(0, 0, angle) * Vector2.right;

        }
        else
        {
            // flipX가 false일 때는 minAngleFlipX에서 maxAngleFlipX 범위로 제한
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360f; // 음수 각도를 양수 각도로 변환
            angle = Mathf.Clamp(angle, 135f, 225f);

            direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        }

        

        // 화살에 방향과 속도를 설정하여 발사합니다.
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * arrowSpeed;
        }
    }

}
