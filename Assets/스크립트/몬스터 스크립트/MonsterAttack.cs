using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public float attackRange = 2.0f;
    public float attackCooldown = 2.0f;
    public int damageAmount = 10; // 몬스터의 공격 데미지
    private Animator animator;
    
    private float nextAttackTime = 0.0f;

    private CharacterHealth playerHealth; // 플레이어의 CharacterHealth 스크립트를 연결

    private void Start()
    {
        animator = GetComponent<Animator>();
        // "Player" 태그를 가진 GameObject를 찾아 CharacterHealth 스크립트를 연결
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<CharacterHealth>();
        }
        else
        {
            Debug.LogError("Player GameObject를 찾을 수 없습니다.");
        }
    }

    public void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            // 여기에 실제 공격 동작을 구현합니다.
            Debug.Log("몬스터가 플레이어를 공격합니다.");

            // 플레이어의 체력을 감소시킴
            playerHealth.TakeDamage(damageAmount);

            // 공격 애니메이션을 시작합니다.
            animator.SetTrigger("Attack"); // "Attack"라는 트리거를 설정하여 공격 애니메이션을 재생합니다.

            // 공격 후 쿨다운 타임 설정
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}
