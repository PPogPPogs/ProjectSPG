using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool isArrow = true; // ȭ�� ���θ� ��Ÿ���� �Ҹ��� ����
    public Transform attackPoint;
    public float attackRange = 5f;
    public LayerMask enemyLayers;
    private bool isStuck = false; // ȭ���� �����Ǿ����� ����
    private Rigidbody2D rb; // ȭ���� Rigidbody2D ������Ʈ

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (!isStuck)
        {
            // ȭ���� ���� �������� ���� ���, ȭ�� ������ �����մϴ�.
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
                monsterHealth.TakeDamage(30); // ������������ ���� ����
            }
            BossMonsterHealth bossMonsterHealth = hitEnemy.GetComponent<BossMonsterHealth>();
            if (bossMonsterHealth != null)
            {
                bossMonsterHealth.TakeDamage(10); // ������������ ���� ����
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
                // ȭ���� ������ �� isArrow ������ false�� �����Ͽ� �߰� ������ �����մϴ�.
                isArrow = false;

                // ȭ���� Rigidbody2D�� �߷��� �����ϰ� ��� �����Ӱ� ȸ���� �󸳴ϴ�.
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.gravityScale = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

                // ȭ���� ���� ������ ��ġ��ŵ�ϴ�.

                // ȭ���� ���� ���·� ǥ���մϴ�.
                isStuck = true;

                // �ݶ��̴� ��Ȱ��ȭ
                Collider2D[] colliders = GetComponents<Collider2D>();
                foreach (Collider2D collider in colliders)
                {
                    collider.enabled = false;
                }
            }
            else if (otherObject.CompareTag("Enemy"))
            {
                // ���Ϳ� �浹�� ���, ȭ���� ������ �ڽ����� ����
                isArrow = false;
                isStuck = true;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.gravityScale = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

                // �ݶ��̴� ��Ȱ��ȭ
                Collider2D[] colliders = GetComponents<Collider2D>();
                foreach (Collider2D collider in colliders)
                {
                    collider.enabled = false;
                }

                // ȭ���� ������ �ڽ����� ����
                transform.SetParent(other.transform, true);
                DealDamage();
            }
        }
    }

}