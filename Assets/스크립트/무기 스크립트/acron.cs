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
        if (collision2D.gameObject.CompareTag("Enemy")) // 'collision'을 사용하여 충돌한 게임 오브젝트를 가져옵니다.
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
                monsterHealth.TakeDamage(30); // 데미지량으로 조절 가능
            }
            BossMonsterHealth bossMonsterHealth = hitEnemy.GetComponent<BossMonsterHealth>();
            if (bossMonsterHealth != null)
            {
                bossMonsterHealth.TakeDamage(10); // 데미지량으로 조절 가능
            }
        }
    }
}
