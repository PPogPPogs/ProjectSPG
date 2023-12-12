using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
           Break();
        }
        else
        {
           // animator.SetTrigger("TakeDamage");
            Debug.Log("ÇÇ ´âÀ½");
            
        }
    }

    private void Break()
    {
       
        Destroy(gameObject);
        
       
    }
}
