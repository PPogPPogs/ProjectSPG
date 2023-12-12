using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float detectionRange = 10.0f; // 타워 감지 범위
    public float attackRange = 2.0f;
    public float attackCooldown = 2.0f;
    public int damageAmount = 10; // 몬스터의 공격 데미지

    private Transform target; // 가장 가까운 타워의 Transform
    private Animator animator;
    private bool isDie = false;
    private float nextAttackTime = 0.0f;

    private MonsterMovement monsterMovement;

    private static List<MonsterAI> MonsterList = new List<MonsterAI>();

    public static List<MonsterAI> GetGodonList()
    {
        return MonsterList;
    }

    private void Start()
    {
        MonsterList.Add(this);
        animator = GetComponent<Animator>();
        monsterMovement = GetComponent<MonsterMovement>();
        FindNearestTower();
    }


    private void Update()
    {
        SaveGodonPosition(transform.position);
        if (target == null)
        {
            FindNearestTower();
        }

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= detectionRange)
            {
                monsterMovement.StopMovement();
                AttackTower();
            }
            else if (distanceToTarget > detectionRange)
            {
                monsterMovement.ResumeMovement();
            }
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
                target = tower.transform;
            }
        }
    }

    public void AttackTower()
    {
        Wall wall = target.GetComponent<Wall>();

        if (!isDie && Time.time >= nextAttackTime)
        {
            if (wall != null)
            {
                // 타워의 체력을 감소시킴
                wall.TakeDamage(damageAmount);

                // 공격 애니메이션을 시작합니다.
                animator.SetTrigger("Attack");

                // 공격 후 쿨다운 타임 설정
                nextAttackTime = Time.time + attackCooldown;
            }
            else
            {
                Debug.LogError("타워에 필요한 Wall 스크립트를 찾을 수 없습니다.");
            }
        }
    }

    public void Die()
    {
        isDie = true;
    }

    private void SaveGodonPosition(Vector3 position)
    {
        string monsterPosition = "MonsterPosition_" + GetInstanceID(); // Unique key for each monster
        PlayerPrefs.SetFloat(monsterPosition + "_X", position.x);
        PlayerPrefs.SetFloat(monsterPosition + "_Y", position.y);
        PlayerPrefs.SetFloat(monsterPosition + "_Z", position.z);
        PlayerPrefs.Save();
    }

    public Vector3 LoadGodonPosition()
    {
        string monsterPosition = "MonsterPosition_" + GetInstanceID();
        float x = PlayerPrefs.GetFloat(monsterPosition + "_X", 0f);
        float y = PlayerPrefs.GetFloat(monsterPosition + "_Y", -0.63f);
        float z = PlayerPrefs.GetFloat(monsterPosition + "_Z", 0f);
        return new Vector3(x, y, z);
    }
}
