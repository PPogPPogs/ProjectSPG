using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMonster : MonoBehaviour
{
    public float MoveSpeed = 1.0f; // �ּ� �̵� �ӵ�
    public float attackRange = 2.0f;
    public int damageAmount = 10;
    public string targetTag = "Castle"; // Ÿ�� ������Ʈ�� �±�
    public float detectionRange = 10.0f;
    private Transform target; // �̵��� ��ǥ ��ġ
    private bool isMoving = true; // �̵� ���� ���¸� ��Ÿ���� ����
    private Animator animator;
    

   
 

    private void Start()
    {
        // �±׷� Ÿ���� ã�� �Ҵ��մϴ�.
        target = GameObject.FindGameObjectWithTag(targetTag)?.transform;

        animator = GetComponent<Animator>();


    }

    private void Update()
    {
        if (target != null && isMoving)
        {
            // ���Ͱ� ��ǥ ��ġ�� �̵��մϴ�.
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // ���� �̵� ����
            transform.Translate(direction.normalized * MoveSpeed * Time.deltaTime);
        }
        animator.SetBool("IsMoving", isMoving);

        float distanceToCaslte = Vector3.Distance(transform.position, target.position);
        if (distanceToCaslte <= detectionRange)
        {
            AttackCastle();
        }

        
    }

    public void StopMovement()
    {
        isMoving = false; // �̵� ����
    }

    public void ResumeMovement()
    {
        isMoving = true; // �̵� �簳
    }

   public void TakeDamage()
    {
        animator.SetTrigger("Die");
    }

   
    public void AttackCastle()
    {
      

                // ���� �ִϸ��̼��� �����մϴ�.
        animator.SetTrigger("Attack"); // "Attack"��� Ʈ���Ÿ� �����Ͽ� ���� �ִϸ��̼��� ����մϴ�.

    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
