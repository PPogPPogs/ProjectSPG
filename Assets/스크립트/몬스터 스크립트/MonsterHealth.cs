using UnityEngine;
using UnityEngine.UI;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthSlider; // ���� ü���� ǥ���ϴ� UI ���
    public Vector3 healthBarOffset = new Vector3(0f, 1.5f, 0f); // ü�¹� ��ġ ���� ������

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthSlider.value = healthPercentage;
    }

    private void LateUpdate()
    {
        UpdateHealthBarPosition();
    }

    private void UpdateHealthBarPosition()
    {
        Vector3 healthBarPosition = transform.position + healthBarOffset;
        healthSlider.transform.position = healthBarPosition;
    }
}

