using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;
	public GameObject coinPrefab;
	private Animator animator;
    public float coinForce = 10f; // 힘의 크기를 조절할 수 있도록 변수를 선언하고 초기값을 설정합니다.
    private MonsterMovement monsterMovement; // 개별 몬스터의 이동 상태를 저장하는 변수

	private void Start()
	{
		currentHealth = maxHealth;
		animator = GetComponent<Animator>();
		monsterMovement = GetComponent<MonsterMovement>(); // MonsterMovement 스크립트 가져오기
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
			monsterMovement.StopMovement(); // 해당 몬스터의 이동 중지
			Debug.Log("체력이 감소했습니다"); // 체력이 감소할 때마다 체력바 및 텍스트 업데이트
		}
	}

	private void Die()
	{
		MonsterPortal monsterPortal = FindObjectOfType<MonsterPortal>();
		if (monsterPortal != null)
		{
			monsterPortal.MonsterKilled();
		}
		monsterMovement.ResumeMovement(); // 해당 몬스터의 이동 재개
		GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

		// 코인을 랜덤한 방향으로 떨어뜨릴 수 있도록 속력을 설정합니다.
		Rigidbody coinRigidbody = coin.GetComponent<Rigidbody>();
		if (coinRigidbody != null)
		{
			Vector3 randomForce = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
			coinRigidbody.AddForce(randomForce * coinForce, ForceMode.Impulse);
		}

		Destroy(gameObject);
	}
}
