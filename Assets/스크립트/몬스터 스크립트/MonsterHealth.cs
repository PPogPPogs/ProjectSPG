using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject coinPrefab;
    private Animator animator;
    public float coinForce = 10f;
    public float coinDropProbability = 0.5f;

    private MonsterMovement monsterMovement;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        monsterMovement = GetComponent<MonsterMovement>();
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

    private void Die()
    {
        Monstersummon monstersummon = FindObjectOfType<Monstersummon>();
        MonsterAttack monsterAttack = FindObjectOfType<MonsterAttack>();
        if (monstersummon != null)
        {
            monstersummon.Onemonsterkilled();
        }
        monsterMovement.ResumeMovement();
        animator.SetTrigger("Die");
        monsterAttack.Die();
    }

    public void Destroy()
    {
        if (Random.value <= coinDropProbability)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

            Rigidbody coinRigidbody = coin.GetComponent<Rigidbody>();
            if (coinRigidbody != null)
            {
                Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
                coinRigidbody.AddForce(randomDirection * coinForce, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }

    // 추가: 외부에서 체력을 설정하는 메서드
    public void SetHealth(int health)
    {
        currentHealth = health;
        // 필요한 경우 추가적인 처리를 수행할 수 있습니다.
    }

    // 추가: 현재 체력을 가져오는 메서드
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
