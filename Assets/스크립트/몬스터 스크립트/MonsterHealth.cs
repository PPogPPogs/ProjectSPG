using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        MonsterMovement monsterMovement = FindObjectOfType<MonsterMovement>();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("TakeDamage");
            monsterMovement.StopMovement();
            Debug.Log("체력이 감소했습니다");// 체력이 감소할 때마다 체력바 및 텍스트 업데이트
        }
    }

    private void Die()
    {
        MonsterPortal monsterPortal = FindObjectOfType<MonsterPortal>();
        if (monsterPortal != null)
        {
            monsterPortal.MonsterKilled();
        }
        Destroy(gameObject);
    }
}
