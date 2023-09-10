using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool isArrow = true; // 화살 여부를 나타내는 불리언 변수
    public Transform attackPoint;
    public float attackRange = 5f;
    public LayerMask enemyLayers;
    private bool isStuck = false; // 화살이 고정되었는지 여부
    private Rigidbody2D rb; // 화살의 Rigidbody2D 컴포넌트

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (!isStuck)
        {
            // 화살이 아직 고정되지 않은 경우, 화살 방향을 설정합니다.
            transform.right = rb.velocity;
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isArrow && !isStuck)
        {
            GameObject otherObject = other.gameObject;

            if (otherObject.CompareTag("Ground"))
            {
                // 화살을 생성한 후 isArrow 변수를 false로 설정하여 추가 생성을 방지합니다.
                isArrow = false;

                // 화살의 Rigidbody2D를 중력을 제외하고 모든 움직임과 회전을 얼립니다.
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.gravityScale = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

                // 화살을 고정 지점에 위치시킵니다.

                // 화살을 고정 상태로 표시합니다.
                isStuck = true;

                // 콜라이더 비활성화
                Collider2D[] colliders = GetComponents<Collider2D>();
                foreach (Collider2D collider in colliders)
                {
                    collider.enabled = false;
                }
            }
            else if (otherObject.CompareTag("Enemy"))
            {
                // 몬스터와 충돌한 경우, 화살을 몬스터의 자식으로 설정
                isArrow = false;
                isStuck = true;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.gravityScale = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

                // 콜라이더 비활성화
                Collider2D[] colliders = GetComponents<Collider2D>();
                foreach (Collider2D collider in colliders)
                {
                    collider.enabled = false;
                }

                // 화살을 몬스터의 자식으로 설정
                transform.SetParent(other.transform, true);
                DealDamage();
            }
        }
    }

}