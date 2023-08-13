using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float detectionRange = 10.0f; // 플레이어 감지 범위
    private Transform player; // 플레이어의 Transform
    private MonsterAttack monsterAttack;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        monsterAttack = GetComponent<MonsterAttack>();
    }

    private void Update()
    {
        // 플레이어 감지 로직
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            // 플레이어를 감지하면 공격
            monsterAttack.AttackPlayer();
        }
    }
}
