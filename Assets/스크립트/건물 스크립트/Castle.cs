using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        // ���� ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
        currentHealth = maxHealth;
    }

    void TakeDamage(int damageAmount)
    {
        // ���ظ� ���� �� ȣ��Ǵ� �޼���
        currentHealth -= damageAmount;

        // ü���� 0 ���Ϸ� �������� ���� ó��
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ĳ������ ��� ó�� (��: ���� ���� ȭ�� ǥ�� ��)
        Debug.Log("Player died");
    }

    void Update()
    {
        // �� �����Ӹ��� ü���� Ȯ���ϰ� �ʿ信 ���� ó��
        if (currentHealth < maxHealth * 0.5f)
        {
            // ü���� �ִ� ü���� ���� �̸����� �������� ���� ó��
            Debug.Log("Health is below 50%");
        }

        else if (currentHealth < maxHealth * 0.25f)
        {
            // ü���� �ִ� ü���� 25% �̸����� �������� ���� ó��
            Debug.Log("Health is below 25%");
        }
    }
}
