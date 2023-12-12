using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMonster : MonoBehaviour
{
    public float MoveSpeed = 1.0f; // 최소 이동 속도
    public float attackRange = 2.0f;
    public int damageAmount = 10;
    public string targetTag = "Castle"; // 타겟 오브젝트의 태그
    public float detectionRange = 10.0f;
    private Transform target; // 이동할 목표 위치
    private bool isMoving = true; // 이동 가능 상태를 나타내는 변수
    private Animator animator;
    

   
 

    private void Start()
    {
        // 태그로 타겟을 찾아 할당합니다.
        target = GameObject.FindGameObjectWithTag(targetTag)?.transform;

        animator = GetComponent<Animator>();


    }

    private void Update()
    {
        if (target != null && isMoving)
        {
            // 몬스터가 목표 위치로 이동합니다.
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // 수직 이동 방지
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
        isMoving = false; // 이동 중지
    }

    public void ResumeMovement()
    {
        isMoving = true; // 이동 재개
    }

   public void TakeDamage()
    {
        animator.SetTrigger("Die");
    }

   
    public void AttackCastle()
    {
      

                // 공격 애니메이션을 시작합니다.
        animator.SetTrigger("Attack"); // "Attack"라는 트리거를 설정하여 공격 애니메이션을 재생합니다.

    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
