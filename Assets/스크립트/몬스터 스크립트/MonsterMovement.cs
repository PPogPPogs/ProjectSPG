
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public string targetTag = "Castle"; // Ÿ�� ������Ʈ�� �±�

    private Transform target; // �̵��� ��ǥ ��ġ

    private void Start()
    {
        // �±׷� Ÿ���� ã�� �Ҵ��մϴ�.
        target = GameObject.FindGameObjectWithTag(targetTag)?.transform;
    }

    private void Update()
    {
        if (target != null)
        {
            // ���Ͱ� ��ǥ ��ġ�� �̵��մϴ�.
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // ���� �̵� ����
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
