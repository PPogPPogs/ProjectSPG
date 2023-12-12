using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAi1 : MonoBehaviour
{
    public float detectionRange = 10.0f; // 플레이어 감지 범위
    private Transform nearestTower; // 가장 가까운 타워의 Transform
    private MonsterAttack monsterAttack;
    private MonsterMovement monsterMovement;

    private void Start()
    {
        FindNearestTower();
        monsterAttack = GetComponent<MonsterAttack>();
        monsterMovement = GetComponent<MonsterMovement>();
    }

    private void Update()
    {
        // 플레이어 감지 로직
        float distanceToNearestTower = Vector3.Distance(transform.position, nearestTower.position);
        if (distanceToNearestTower <= detectionRange)
        {
            monsterMovement.StopMovement();
            monsterAttack.AttackTower();
        }
        else if (distanceToNearestTower > detectionRange)
        {
            monsterMovement.ResumeMovement();
        }
    }

    private void FindNearestTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("DefenseTower");
        float minDistance = float.MaxValue;

        foreach (GameObject tower in towers)
        {
            float distance = Vector3.Distance(transform.position, tower.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTower = tower.transform;
            }
        }
    }
}
