using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float minMoveSpeed = 1.0f; // �ּ� �̵� �ӵ�
    public float maxMoveSpeed = 2.0f; // �ִ� �̵� �ӵ�
    public string targetTag = "Castle"; // Ÿ�� ������Ʈ�� �±�

    private Transform target; // �̵��� ��ǥ ��ġ
    private bool isMoving = true; // �̵� ���� ���¸� ��Ÿ���� ����
    private Animator animator;
    private float moveSpeed;

    // ���� ������ �̵� ���¸� �����ϴ� ����Ʈ
    private static List<MonsterMovement> monsters = new List<MonsterMovement>();

    private void Start()
    {
        // �±׷� Ÿ���� ã�� �Ҵ��մϴ�.
        target = GameObject.FindGameObjectWithTag(targetTag)?.transform;
        animator = GetComponent < Animator>();

        // ������ �̵� �ӵ��� �������� ����
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        // ����Ʈ�� ���� ���͸� �߰��մϴ�.
        monsters.Add(this);
    }

    private void Update()
    {
        if (target != null && isMoving)
        {
            // ���Ͱ� ��ǥ ��ġ�� �̵��մϴ�.
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // ���� �̵� ����
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        }
        animator.SetBool("IsMoving", isMoving);
    }

    public void StopMovement()
    {
        isMoving = false; // �̵� ����
    }

    public void ResumeMovement()
    {
        isMoving = true; // �̵� �簳
    }

    // ��� ���Ϳ� ���� �̵��� �簳�ϵ��� �ϴ� �Լ�
    public static void ResumeAllMonsters()
    {
        foreach (var monster in monsters)
        {
            monster.ResumeMovement();
        }
    }

    // ���� ���� ���� �� ����Ʈ���� ����
    private void OnDestroy()
    {
        monsters.Remove(this);
    }
}
