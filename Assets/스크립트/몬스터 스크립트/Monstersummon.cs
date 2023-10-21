using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstersummon : MonoBehaviour
{
    public GameObject clearText; // 정화 택스트
    private bool isone = true;
    private bool istwo = false;
    private bool isthree = false;
    private bool isInRange = false;
    private int Monsterkilled = 0;
    private Animator animator;
    private MonsterSpawn1 monsterSpawn1;


    // Start is called before the first frame update
    void Start()
    {
        monsterSpawn1 = FindObjectOfType<MonsterSpawn1>();
        animator = GetComponent<Animator>();
        isone = PlayerPrefs.GetInt("IsOne", 1) == 1;
        istwo = PlayerPrefs.GetInt("IsTwo", 0) == 1;
        isthree = PlayerPrefs.GetInt("IsThree", 0) == 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Monsterkilled == 20)
        {
            isInRange = true;
            clearText.SetActive(true);
        }

        else if (other.CompareTag("Player") && Monsterkilled == 41)
        {
            isInRange = true;
            clearText.SetActive(true);
        }

        else if (other.CompareTag("Player") && Monsterkilled == 62)
        {
            isInRange = true;
            clearText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            clearText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.T) && isone)
        {
            Oneclear();
        }

        else if (isInRange && Input.GetKeyDown(KeyCode.T) && istwo)
        {
            Twoclear();
        }

        else if (isInRange && Input.GetKeyDown(KeyCode.T) && isthree)
        {
            Threeclear();
        }

        animator.SetBool("IsOne", isone);
        animator.SetBool("IsTwo", istwo);
        animator.SetBool("IsThree", isthree);

    }

    private void Oneclear()
    {
        isone = false;
        istwo = true;
        PlayerPrefs.SetInt("IsOne", 0);
        PlayerPrefs.SetInt("IsTwo", 1);
        PlayerPrefs.Save();
        Monsterkilled++;
        monsterSpawn1.FirstMonsterDead();
    }

    private void Twoclear()
    {
        istwo = false;
        isthree = true;
        PlayerPrefs.SetInt("IsTwo", 0);
        PlayerPrefs.SetInt("IsThree", 1);
        PlayerPrefs.Save();
        Monsterkilled++;
        monsterSpawn1.SecondMonsterDead();
    }

    private void Threeclear()
    {
        isthree = false;
       
        PlayerPrefs.SetInt("IsThree", 0);
       
        PlayerPrefs.Save();
        monsterSpawn1.ThirdMonsterDead();

        animator.SetTrigger("Destroy");
        Destroy(gameObject, 5f);

    }


    public void Onemonsterkilled()
    {
        Monsterkilled++;
    }
}
