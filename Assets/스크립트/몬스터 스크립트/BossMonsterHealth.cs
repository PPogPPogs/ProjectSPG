using UnityEngine;
using UnityEngine.UI;

public class BossMonsterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider; // UI Slider 요소를 연결
    public Text healthText; // UI Text 요소를 연결

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar(); // 시작할 때 체력바 및 텍스트 업데이트
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
            UpdateHealthBar();
            Debug.Log("체력이 감소했습니다");// 체력이 감소할 때마다 체력바 및 텍스트 업데이트
        }
    }

    private void Die()
    {
        MonsterPortal monsterPortal = FindObjectOfType<MonsterPortal>();
        if (monsterPortal != null)
        {
            monsterPortal.MonsterKilled();
        }
        Destroy(gameObject);
    }

    public void UpdateHealthBar()
    {
        healthSlider.value = (float)currentHealth / maxHealth;
        // 체력바 업데이트
        healthText.text = "BOSS";
        // 텍스트 업데이트
    }
}
