using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    public float attackRange = 2.0f;
    public float attackCooldown = 2.0f;
    public int damageAmount = 10; // ������ ���� ������

    private float nextAttackTime = 0.0f;

    private CharacterHealth playerHealth; // �÷��̾��� CharacterHealth ��ũ��Ʈ�� ����

    private void Start()
    {
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

    public void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            // ���⿡ ���� ���� ������ �����մϴ�.
            Debug.Log("���Ͱ� �÷��̾ �����մϴ�.");

            // �÷��̾��� ü���� ���ҽ�Ŵ
            playerHealth.TakeDamage(damageAmount);

            // ���� �� ��ٿ� Ÿ�� ����
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}
