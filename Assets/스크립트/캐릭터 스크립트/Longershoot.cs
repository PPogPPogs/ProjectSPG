using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longershoot : MonoBehaviour
{
    private Transform monster;
    public GameObject arrowPrefab; // 화살 프리팹
    public float arrowSpeed = 10f; // 화살 발사 속도
    public float gravity = 9.81f; // 중력 가속도
    private float nextAttackTime = 0.0f;
    private float currentAttackCooldown = 0.0f;
    public float minAttackCooldown = 2.0f; // 최소 공격 쿨타임
    public float maxAttackCooldown = 5.0f; // 최대 공격 쿨타임
    private Animator animator;



    private void Start()
    {
        animator = GetComponent<Animator>();
    }



    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("Attack");
            Vector3 direction = monster.position - transform.position;
            float distance = direction.magnitude;

            // 포물선 운동 계산
            float time = distance / arrowSpeed;
            float verticalSpeed = arrowSpeed - (gravity * time) / 2;
            Vector3 initialVelocity = direction.normalized * arrowSpeed;
            initialVelocity.y = verticalSpeed;

            // 화살 발사
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
            arrowRb.velocity = initialVelocity;
            // 여기에 실제 공격 로직을 구현
            Debug.Log("몬스터가 플레이어를 공격합니다!");
            currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
            nextAttackTime = Time.time + currentAttackCooldown;
        }
    }
}