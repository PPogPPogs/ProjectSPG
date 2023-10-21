using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
  
    private void Start()
    {
        currentHealth = PlayerPrefs.GetInt("PlayerHealth", maxHealth);
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
       
    }

    private void Die()
    {
        Debug.Log("Player died!");
        currentHealth = maxHealth;
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
       
    }

    
}
