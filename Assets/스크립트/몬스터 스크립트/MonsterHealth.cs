using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;
	public GameObject coinPrefab;
	private Animator animator;
    public float coinForce = 10f; // ���� ũ�⸦ ������ �� �ֵ��� ������ �����ϰ� �ʱⰪ�� �����մϴ�.
    private MonsterMovement monsterMovement; // ���� ������ �̵� ���¸� �����ϴ� ����

	private void Start()
	{
		currentHealth = maxHealth;
		animator = GetComponent<Animator>();
		monsterMovement = GetComponent<MonsterMovement>(); // MonsterMovement ��ũ��Ʈ ��������
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
			monsterMovement.StopMovement(); // �ش� ������ �̵� ����
			Debug.Log("ü���� �����߽��ϴ�"); // ü���� ������ ������ ü�¹� �� �ؽ�Ʈ ������Ʈ
		}
	}

	private void Die()
	{
		MonsterPortal monsterPortal = FindObjectOfType<MonsterPortal>();
		if (monsterPortal != null)
		{
			monsterPortal.MonsterKilled();
		}
		monsterMovement.ResumeMovement(); // �ش� ������ �̵� �簳
		GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

		// ������ ������ �������� ����߸� �� �ֵ��� �ӷ��� �����մϴ�.
		Rigidbody coinRigidbody = coin.GetComponent<Rigidbody>();
		if (coinRigidbody != null)
		{
			Vector3 randomForce = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
			coinRigidbody.AddForce(randomForce * coinForce, ForceMode.Impulse);
		}

		Destroy(gameObject);
	}
}
