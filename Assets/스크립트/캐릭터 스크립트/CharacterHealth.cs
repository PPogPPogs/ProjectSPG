using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider; // UI Slider 요소를 연결
    public Text healthText; // UI Text 요소를 연결

    private void Start()
    {
        currentHealth = PlayerPrefs.GetInt("PlayerHealth", maxHealth);
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
            PlayerPrefs.SetInt("PlayerHealth", currentHealth);
            UpdateHealthBar(); // 체력이 감소할 때마다 체력바 및 텍스트 업데이트
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        currentHealth = maxHealth;
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        UpdateHealthBar(); // 사망할 때 체력바 및 텍스트 업데이트
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = (float)currentHealth / maxHealth; // 체력바 업데이트
        healthText.text = "Health: " + currentHealth.ToString(); // 텍스트 업데이트
    }
}
