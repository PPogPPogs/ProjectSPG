using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acron : MonoBehaviour
{
    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRange = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Enemy")) // 'collision'�� ����Ͽ� �浹�� ���� ������Ʈ�� �����ɴϴ�.
        {
            DealDamage();
        }
    }


    public void DealDamage()
    {
        Collider2D hitEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
        if (hitEnemy != null)
        {
            MonsterHealth monsterHealth = hitEnemy.GetComponent<MonsterHealth>();
            if (monsterHealth != null)
            {
                monsterHealth.TakeDamage(30); // ������������ ���� ����
            }
            BossMonsterHealth bossMonsterHealth = hitEnemy.GetComponent<BossMonsterHealth>();
            if (bossMonsterHealth != null)
            {
                bossMonsterHealth.TakeDamage(10); // ������������ ���� ����
            }
        }
    }
}
