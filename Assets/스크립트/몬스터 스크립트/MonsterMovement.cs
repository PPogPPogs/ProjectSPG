
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public string targetTag = "Castle"; // 타겟 오브젝트의 태그

    private Transform target; // 이동할 목표 위치

    private void Start()
    {
        // 태그로 타겟을 찾아 할당합니다.
        target = GameObject.FindGameObjectWithTag(targetTag)?.transform;
    }

    private void Update()
    {
        if (target != null)
        {
            // 몬스터가 목표 위치로 이동합니다.
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // 수직 이동 방지
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
