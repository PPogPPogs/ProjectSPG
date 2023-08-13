using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider; // UI Slider ��Ҹ� ����
    public Text healthText; // UI Text ��Ҹ� ����

    private void Start()
    {
        currentHealth = PlayerPrefs.GetInt("PlayerHealth", maxHealth);
        UpdateHealthBar(); // ������ �� ü�¹� �� �ؽ�Ʈ ������Ʈ
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
            UpdateHealthBar(); // ü���� ������ ������ ü�¹� �� �ؽ�Ʈ ������Ʈ
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        currentHealth = maxHealth;
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        UpdateHealthBar(); // ����� �� ü�¹� �� �ؽ�Ʈ ������Ʈ
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = (float)currentHealth / maxHealth; // ü�¹� ������Ʈ
        healthText.text = "Health: " + currentHealth.ToString(); // �ؽ�Ʈ ������Ʈ
    }
}
