using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longerunit : MonoBehaviour
{
    public float detectionRange = 10.0f; // �÷��̾� ���� ����
    private Transform monster; // �÷��̾��� Transform
    public Transform targetPosition; // �̵���ų ��ǥ ��ġ�� ������ Ʈ������
    public float moveSpeed = 5.0f; // �̵� �ӵ�
    public float minAttackCooldown = 2.0f; // �ּ� ���� ��Ÿ��
    public float maxAttackCooldown = 5.0f; // �ִ� ���� ��Ÿ��
    private float currentAttackCooldown = 0.0f;
    private bool canAttack = true;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        monster = GameObject.FindGameObjectWithTag("Enemy").transform;
        CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
        if (calendarManager != null)
        {
            int currentHour = calendarManager.GetHour();
            if (currentHour == 17)
            {
                Movewall();
            }
        }
        // �÷��̾� ���� ����

        float distanceToMonster = Vector3.Distance(transform.position, monster.position);

        if (distanceToMonster <= detectionRange)
        {
            if (canAttack)
            {
                // ���Ͱ� ���� ���� ���� �ְ�, ���� ������ ������ �� ������ ����
                Attack();
                // ������ ���� ��Ÿ���� ����
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                canAttack = false;
            }
            else
            {
                // ���� ��Ÿ���� ����
                currentAttackCooldown -= Time.deltaTime;

                if (currentAttackCooldown <= 0)
                {
                    // ��Ÿ���� ������ �� �ٽ� ���� ���� ���·� ����
                    canAttack = true;
                }
            }
        }
        else if (distanceToMonster > detectionRange)
        {
            // ���Ͱ� ���� ���� �ۿ� �ִٸ� �̵���ų ��ǥ ��ġ�� �̵�
            
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        // ���⿡ ���� ���� ������ ����
        Debug.Log("���Ͱ� �÷��̾ �����մϴ�!");
    }

    private void Movewall()
    {
        // �̵� ���� ����
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
    }
}
