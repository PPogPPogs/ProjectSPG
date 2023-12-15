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
    public int maxHealth = 100;
    public int currentHealth;
    public int goldReward = 1;

    private static List<LeftMonster> LeftmonsterList = new List<LeftMonster>();



    private void Start()
    {
        LeftmonsterList.Add(this);
        // 태그로 타겟을 찾아 할당합니다.
        target = GameObject.FindGameObjectWithTag(targetTag)?.transform;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();


    }

    public static List<LeftMonster> GetLeftmonsterList()
    {
        return LeftmonsterList;
    }


    private void Update()
    {
        SaveLeftmonsterPosition(transform.position);
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("TakeDamage");
            Debug.Log("체력이 감소했습니다");
        }
    }

    public void AttackCastle()
    {
      

                // 공격 애니메이션을 시작합니다.
        animator.SetTrigger("Attack"); // "Attack"라는 트리거를 설정하여 공격 애니메이션을 재생합니다.

    }

    public void Die()
    {
        {
            CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
            if (currencyManager != null)
            {
                currencyManager.AddCurrency(Currency.CurrencyType.Gold, goldReward);

            }
        }
        Destroy(gameObject);
        //animator.SetTrigger("Die");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }


    private void SaveLeftmonsterPosition(Vector3 position)
    {
        string leftmonsterPosition = "LeftMonsterPosition_" + GetInstanceID(); // Unique key for each monster
        PlayerPrefs.SetFloat(leftmonsterPosition + "_X", position.x);
        PlayerPrefs.SetFloat(leftmonsterPosition + "_Y", position.y);
        PlayerPrefs.SetFloat(leftmonsterPosition + "_Z", position.z);
        PlayerPrefs.Save();
    }

    public Vector3 LoadLeftmonsterPosition()
    {
        string leftmonsterPosition = "LeftMonsterPosition_" + GetInstanceID();
        float x = PlayerPrefs.GetFloat(leftmonsterPosition + "_X", 0f);
        float y = PlayerPrefs.GetFloat(leftmonsterPosition + "_Y", -0.63f);
        float z = PlayerPrefs.GetFloat(leftmonsterPosition + "_Z", 0f);
        return new Vector3(x, y, z);
    }

}
