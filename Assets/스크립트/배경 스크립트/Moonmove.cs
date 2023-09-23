using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moonmove : MonoBehaviour
{
    public Transform center;    // ��� �߽���
    public float radius = 2.0f; // ������
    public float speed = 2.0f;  // �ӵ�
    public Transform background; // ��� Transform
    public float scrollSpeed = 1.0f; // ��ũ�� �ӵ�
    public CalendarManager calendarManager; // Ķ���� ������ �߰�

    private float angle = 0;
    private Vector3 initialBackgroundPosition;
    private Vector3 initialPlayerPosition;

    private void Start()
    {
        initialBackgroundPosition = background.position;
        initialPlayerPosition = transform.position; // �÷��̾��� �ʱ� ��ġ ����

    }



    private void Update()
    {
        // �÷��̾��� ������ ��ȭ�� ���
        float movementDelta = transform.position.x - initialPlayerPosition.x;

        // ����� x �������� ��ũ��
        Vector3 backgroundPosition = initialBackgroundPosition;
        backgroundPosition.x += movementDelta * scrollSpeed;
        background.position = backgroundPosition;

        if (calendarManager != null)
        {
            float gameTimeInSeconds = calendarManager.GetGameTimeInSeconds();
            float hours = gameTimeInSeconds / 3600f;
            float initialAngleOffset = (hours - 19) * (2 * Mathf.PI / 24); // 12�ø� �������� ���� ������ ���
            angle = initialAngleOffset;
        }

        // �÷��̾ ���
        angle += speed * Time.deltaTime;
        transform.position = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
    }
}
