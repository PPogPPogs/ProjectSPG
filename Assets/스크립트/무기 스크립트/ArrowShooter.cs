using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // ȭ�� �������� �Ҵ��ϼ���.
    public Transform arrowSpawnPoint; // ȭ���� �߻�� ��ġ�� �Ҵ��ϼ���.
    public float arrowSpeed = 10.0f;
    private float minAngle = -45f; // �ּ� ���� (flipX�� true�� ��)
    private float maxAngle = 45f; // �ִ� ���� (flipX�� true�� ��)
    private PlayerController playerController;
    private bool isAiming = false;

    private void Start()
    {
        // PlayerController ��ũ��Ʈ�� ���� ������Ʈ�� ã�� �����մϴ�.
        playerController = FindObjectOfType<PlayerController>();
        
    }

    private void Update()
    {
        // ���콺 ��Ŭ�� ����
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
        }
        else if (Input.GetMouseButtonUp(1))
            {
            isAiming = false;
        }
       
        

        // ���콺 ��Ŭ�� ���¿��� ��Ŭ������ ȭ�� �߻�
        if (isAiming && Input.GetMouseButtonUp(0))
        {
            ShootArrow();
            
        }
    }

    private void ShootArrow()
    {
        // ȭ���� Ȱ��ȭ��Ű�� �ʱ� ��ġ�� ȸ���� �����մϴ�.
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

        // Ŀ�� ��ġ�� ���� ��ǥ�� ��ȯ�Ͽ� ���� ���� ���
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (cursorPosition - (Vector2)arrowSpawnPoint.position).normalized;

        // ���� ������ ����մϴ�.
        float angle;

        // ���� ������ minAngle �Ǵ� maxAngle ���� ���� �ִٸ� ������ �ش� ������ �����մϴ�.

        if (playerController.GetFlipX())
        {
            // flipX�� true�� ���� minAngle���� maxAngle ������ ����
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, minAngle, maxAngle);
            direction = Quaternion.Euler(0, 0, angle) * Vector2.right;

        }
        else
        {
            // flipX�� false�� ���� minAngleFlipX���� maxAngleFlipX ������ ����
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360f; // ���� ������ ��� ������ ��ȯ
            angle = Mathf.Clamp(angle, 135f, 225f);

            direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        }

        

        // ȭ�쿡 ����� �ӵ��� �����Ͽ� �߻��մϴ�.
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * arrowSpeed;
        }
    }

}
