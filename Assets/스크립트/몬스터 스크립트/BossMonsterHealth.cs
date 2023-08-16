using UnityEngine;
using UnityEngine.UI;

public class BossMonsterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider; // UI Slider ��Ҹ� ����
    public Text healthText; // UI Text ��Ҹ� ����

    private void Start()
    {
        currentHealth = maxHealth;
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
            UpdateHealthBar();
            Debug.Log("ü���� �����߽��ϴ�");// ü���� ������ ������ ü�¹� �� �ؽ�Ʈ ������Ʈ
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
        // ü�¹� ������Ʈ
        healthText.text = "BOSS";
        // �ؽ�Ʈ ������Ʈ
    }
}
