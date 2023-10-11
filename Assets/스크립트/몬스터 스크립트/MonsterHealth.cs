using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject coinPrefab;
    private Animator animator;
    public float coinForce = 10f;
    public float coinDropProbability = 0.5f; // ������ ����߸� Ȯ���� �����մϴ�.
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
            Debug.Log("ü���� �����߽��ϴ�");
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

        // ������ Ȯ���� ������ ����߸��ϴ�.
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

	

