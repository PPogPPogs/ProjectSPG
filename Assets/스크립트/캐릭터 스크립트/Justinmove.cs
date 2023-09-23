using UnityEngine;
using System.Collections;

public class Justinmove : MonoBehaviour
{
    public float speed = 5.0f; // ������ �ӵ�
    public float minX = -5.0f; // �ּ� X ��ġ
    public float maxX = 5.0f; // �ִ� X ��ġ
    public float minChangeDirectionTime = 2.0f; // ���� ���� �ּ� �ð�(��)
    public float maxChangeDirectionTime = 5.0f; // ���� ���� �ִ� �ð�(��)

    private int direction = 1; // �ʱ� ������ ���� (1: ������, -1: ����)
    private Animator animator;
    private Vector3 originalScale;

    private void Start()
    {
        // ó������ ������ �����մϴ�.
        StartCoroutine(ChangeDirection());

        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();

        // ĳ������ �ʱ� ������ ����
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // ���� ��ġ�� �����մϴ�.
        Vector3 currentPosition = transform.position;

        // ������ ���⿡ ���� ��ġ�� �����մϴ�.
        currentPosition.x += direction * speed * Time.deltaTime;

        // ���ο� ��ġ�� �̵��մϴ�.
        transform.position = currentPosition;

        if (currentPosition.x <= minX || currentPosition.x >= maxX)
        {
            direction *= -1; // ������ ������Ŵ

            // ĳ���� �������� ���⿡ ���� �������ϴ�.
            if (direction < 0)
            {
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
            else
            {
                transform.localScale = originalScale;
            }
        }

        // �����ӿ� ���� �ִϸ��̼��� �����մϴ�.
        bool isRunning = Mathf.Abs(direction) > 0;
        animator.SetBool("isRunning", isRunning);
    }

    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            // ������ �ð��� ��ٸ��ϴ�.
            float changeDirectionTime = Random.Range(minChangeDirectionTime, maxChangeDirectionTime);
            yield return new WaitForSeconds(changeDirectionTime);

            // ������ �����ϰ� �����մϴ�.
            direction *= Random.Range(0, 2) == 0 ? 1 : -1; // 0 �Ǵ� 1�� �����ϰ� �����Ͽ� ���� ����

            // ĳ���� �������� ���⿡ ���� �������ϴ�.
            if (direction < 0)
            {
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
            else
            {
                transform.localScale = originalScale;
            }
        }
    }
}
