using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMonster : MonoBehaviour
{
    public float MoveSpeed = 1.0f; // �ּ� �̵� �ӵ�
    public float attackRange = 2.0f;
    public int damageAmount = 10;
    public string targetTag = "Castle"; // Ÿ�� ������Ʈ�� �±�
    public float detectionRange = 10.0f;
    private Transform target; // �̵��� ��ǥ ��ġ
    private bool isMoving = true; // �̵� ���� ���¸� ��Ÿ���� ����
    private Animator animator;
    public int maxHealth = 100;
    public int currentHealth;
    public int goldReward = 1;

    private static List<LeftMonster> LeftmonsterList = new List<LeftMonster>();



    private void Start()
    {
        LeftmonsterList.Add(this);
        // �±׷� Ÿ���� ã�� �Ҵ��մϴ�.
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
            // ���Ͱ� ��ǥ ��ġ�� �̵��մϴ�.
            Vector3 direction = target.position - transform.position;
            direction.y = 0; // ���� �̵� ����
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
        isMoving = false; // �̵� ����
    }

    public void ResumeMovement()
    {
        isMoving = true; // �̵� �簳
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
            Debug.Log("ü���� �����߽��ϴ�");
        }
    }

    public void AttackCastle()
    {
      

                // ���� �ִϸ��̼��� �����մϴ�.
        animator.SetTrigger("Attack"); // "Attack"��� Ʈ���Ÿ� �����Ͽ� ���� �ִϸ��̼��� ����մϴ�.

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
