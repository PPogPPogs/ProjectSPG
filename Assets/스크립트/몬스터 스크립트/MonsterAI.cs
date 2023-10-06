using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float detectionRange = 10.0f; // �÷��̾� ���� ����
    private Transform player; // �÷��̾��� Transform
    private MonsterAttack monsterAttack;
    private MonsterMovement monsterMovement;  

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        monsterAttack = GetComponent<MonsterAttack>();
        monsterMovement = GetComponent<MonsterMovement>();
    }

    private void Update()
    {
        // �÷��̾� ���� ����
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            monsterMovement.StopMovement();
            monsterAttack.AttackPlayer();
        }
        else if(distanceToPlayer > detectionRange)
        {

            monsterMovement.ResumeMovement();
        }
    }
}
