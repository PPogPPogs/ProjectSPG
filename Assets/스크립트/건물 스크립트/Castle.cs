using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        // 게임 시작 시 현재 체력을 최대 체력으로 초기화
        currentHealth = maxHealth;
    }

    void TakeDamage(int damageAmount)
    {
        // 피해를 입을 때 호출되는 메서드
        currentHealth -= damageAmount;

        // 체력이 0 이하로 떨어졌을 때의 처리
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 캐릭터의 사망 처리 (예: 게임 오버 화면 표시 등)
        Debug.Log("Player died");
    }

    void Update()
    {
        // 매 프레임마다 체력을 확인하고 필요에 따라 처리
        if (currentHealth < maxHealth * 0.5f)
        {
            // 체력이 최대 체력의 절반 미만으로 떨어졌을 때의 처리
            Debug.Log("Health is below 50%");
        }

        else if (currentHealth < maxHealth * 0.25f)
        {
            // 체력이 최대 체력의 25% 미만으로 떨어졌을 때의 처리
            Debug.Log("Health is below 25%");
        }
    }
}
