using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float minMoveSpeed = 1.0f; // 최소 이동 속도
    public float maxMoveSpeed = 2.0f; // 최대 이동 속도
    public string targetTag = "Castle"; // 타겟 오브젝트의 태그

    private Transform target; // 이동할 목표 위치
    private bool isMoving = true; // 이동 가능 상태를 나타내는 변수
    private Animator animator;
    private float moveSpeed;

    // 개별 몬스터의 이동 상태를 저장하는 리스트
    private static List<MonsterMovement> monsters = new List<MonsterMovement>();

    private void Start()
    {
        // 태그로 타겟을 찾아 할당합니다.
        target = GameObject.FindGameObjectWithTag(targetTag)?.transform;
        animator = GetComponent < Animator>();

        // 몬스터의 이동 속도를 랜덤으로 설정
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        // 리스트에 현재 몬스터를 추가합니다.
        monsters.Add(this);
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

    // 모든 몬스터에 대해 이동을 재개하도록 하는 함수
    public static void ResumeAllMonsters()
    {
        foreach (var monster in monsters)
        {
            monster.ResumeMovement();
        }
    }

    // 개별 몬스터 제거 시 리스트에서 제거
    private void OnDestroy()
    {
        monsters.Remove(this);
    }
}
