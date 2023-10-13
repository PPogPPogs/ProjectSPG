using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longerunit : MonoBehaviour
{
    public float detectionRange = 10.0f; // 플레이어 감지 범위
    private Transform monster; // 플레이어의 Transform
    public Transform targetPosition; // 이동시킬 목표 위치를 지정할 트랜스폼
    public float moveSpeed = 5.0f; // 이동 속도
    public float minAttackCooldown = 2.0f; // 최소 공격 쿨타임
    public float maxAttackCooldown = 5.0f; // 최대 공격 쿨타임
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
        // 플레이어 감지 로직

        float distanceToMonster = Vector3.Distance(transform.position, monster.position);

        if (distanceToMonster <= detectionRange)
        {
            if (canAttack)
            {
                // 몬스터가 감지 범위 내에 있고, 공격 가능한 상태일 때 공격을 실행
                Attack();
                // 랜덤한 공격 쿨타임을 설정
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                canAttack = false;
            }
            else
            {
                // 공격 쿨타임을 감소
                currentAttackCooldown -= Time.deltaTime;

                if (currentAttackCooldown <= 0)
                {
                    // 쿨타임이 끝났을 때 다시 공격 가능 상태로 변경
                    canAttack = true;
                }
            }
        }
        else if (distanceToMonster > detectionRange)
        {
            // 몬스터가 감지 범위 밖에 있다면 이동시킬 목표 위치로 이동
            
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        // 여기에 실제 공격 로직을 구현
        Debug.Log("몬스터가 플레이어를 공격합니다!");
    }

    private void Movewall()
    {
        // 이동 로직 구현
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
    }
}
