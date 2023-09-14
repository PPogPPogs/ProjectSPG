using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // ȭ�� �������� �Ҵ��ϼ���.
    public Transform arrowSpawnPoint; // ȭ���� �߻�� ��ġ�� �Ҵ��ϼ���.
    public float arrowSpeed = 10.0f;

    private bool isAiming = false;

    private void Update()
    {
        // ���콺 ��Ŭ�� ����
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
        }

        // ���콺 ��Ŭ�� ���¿��� ��Ŭ������ ȭ�� �߻�
        if (isAiming && Input.GetMouseButtonDown(0))
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

        // ȭ�쿡 ����� �ӵ��� �����Ͽ� �߻��մϴ�.
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * arrowSpeed;
        }
    }
}
