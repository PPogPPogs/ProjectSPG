using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public string targetTag = "Castle"; // 타겟 오브젝트의 태그

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
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        }
        animator.SetBool("IsMoving", isMoving);
    }

    public void StopMovement()
    {
        isMoving = false; // 이동 중지
    }

    public void ResumeMovement()
    {
        isMoving = true; // 이동 재개
    }
}
