using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public float attackRange = 2.0f;
    public float attackCooldown = 2.0f;
    public int damageAmount = 10; // ������ ���� ������
    private Animator animator;
    private bool isDie = false;
    
    private float nextAttackTime = 0.0f;

    private CharacterHealth playerHealth; // �÷��̾��� CharacterHealth ��ũ��Ʈ�� ����

    private void Start()
    {
        animator = GetComponent<Animator>();
        // "Player" �±׸� ���� GameObject�� ã�� CharacterHealth ��ũ��Ʈ�� ����
        GameObject player = GameObject.FindWithTag("Player");
      
        if (player != null)
        {
            playerHealth = player.GetComponent<CharacterHealth>();
        }
        else
        {
            Debug.LogError("Player GameObject�� ã�� �� �����ϴ�.");
        }

    }
    public void Die()
    {
        isDie = true;
    }

    public void AttackPlayer()
    {
        if (!isDie)
        {
            if (Time.time >= nextAttackTime)
            {  
                // �÷��̾��� ü���� ���ҽ�Ŵ
                playerHealth.TakeDamage(damageAmount);

                // ���� �ִϸ��̼��� �����մϴ�.
                animator.SetTrigger("Attack"); // "Attack"��� Ʈ���Ÿ� �����Ͽ� ���� �ִϸ��̼��� ����մϴ�.

                // ���� �� ��ٿ� Ÿ�� ����
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    public void AttackTower()
    {
        Wall wall = FindObjectOfType<Wall>();

        if (!isDie)
        {
            if (Time.time >= nextAttackTime)
            {
                if (wall != null)
                {
                    nextAttackTime = Time.time + attackCooldown;
                    Debug.Log("�Ӵ�");
                    // �÷��̾��� ü���� ���ҽ�Ŵ
                    wall.TakeDamage(damageAmount);

                    // ���� �ִϸ��̼��� �����մϴ�.
                    animator.SetTrigger("Attack"); // "Attack"��� Ʈ���Ÿ� �����Ͽ� ���� �ִϸ��̼��� ����մϴ�.

                    // ���� �� ��ٿ� Ÿ�� ����
                    
                }
                else
                {
                    Debug.LogError("Wall ��ũ��Ʈ�� ã�� �� �����ϴ�.");
                }
            }
        }
    }


}
