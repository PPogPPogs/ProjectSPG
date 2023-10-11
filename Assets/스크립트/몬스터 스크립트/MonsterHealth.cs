using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject coinPrefab;
    private Animator animator;
    public float coinForce = 10f;
    public float coinDropProbability = 0.5f; // 코인을 떨어뜨릴 확률을 설정합니다.
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
        if (monstersummon != null)
        {
            monstersummon.Onemonsterkilled();
        }
        monsterMovement.ResumeMovement();

        // 랜덤한 확률로 코인을 떨어뜨립니다.
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
}

	

