using UnityEngine;
using System.Collections;

public class MonsterSpawn4 : MonoBehaviour
{
    // ���� ������ �� ���� ��ġ ����

    public GameObject aamonsterPrefab;
    public GameObject bbmonsterPrefab;
    public GameObject ccmonsterPrefab;
    private bool playerInsideTrigger = false;
    private float spawnDelay = 10f; // ���� ���� ��� �ð�
    private float timer = 0f;
    private bool monstersSpawned = false;
    private bool monsterSSpawned = false;
    private bool isaamonster = true;
    private bool isbbmonster = false;
    private bool isccmonster = false;
    public Transform[] spawnPoints; // ���� ���� ��ġ �迭

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !monstersSpawned)
        {
            playerInsideTrigger = true;

            // Ʈ���� ���� �� ���� ���� ����
            PlayerPrefs.SetInt("IsAaMonster", isaamonster ? 1 : 0);
            PlayerPrefs.SetInt("IsBbMonster", isbbmonster ? 1 : 0);
            PlayerPrefs.SetInt("IsCcMonster", isccmonster ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = false;
            timer = 0f;
            monstersSpawned = false;
        }
    }

    private void Start()
    {
        // ���� ���� �� ����� ���� ���� �ҷ�����
        isaamonster = PlayerPrefs.GetInt("IsAaMonster", 1) == 1;
        isbbmonster = PlayerPrefs.GetInt("IsBbMonster", 0) == 1;
        isccmonster = PlayerPrefs.GetInt("IsCcMonster", 0) == 1;
    }

    private void Update()
    {
        CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
        if (calendarManager != null)
        {
            int currentHour = calendarManager.GetHour();
            int currentDay = calendarManager.GetDay();
            if (currentDay == 32 && currentHour ==19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(3));

                monsterSSpawned = true;

            }

            else if (currentDay == 33 && currentHour == 7 && !monsterSSpawned)
            {
                FirstMonsterDead();
            }

            else if (currentDay == 34 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(3));
                monsterSSpawned = true;
            }

            else if (currentDay == 35 && currentHour == 7 && !monsterSSpawned)
            {
                SecondMonsterDead();
            }

            else if (currentDay == 35 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(3));
                monsterSSpawned = true;
            }

            else if (currentDay == 36 && currentHour == 7 && !monsterSSpawned)
            {
                ThirdMonsterDead();
            }

            if (currentHour == 19)
            {
                monsterSSpawned = false;
            }


        }

    }

    public void FirstMonsterDead()
    {
        isaamonster = false;
        isbbmonster = true;

        // ���� ���� ���� �� ����
        PlayerPrefs.SetInt("IsAaMonster", 0);
        PlayerPrefs.SetInt("IsBbMonster", 1);
        PlayerPrefs.Save();
    }

    public void SecondMonsterDead()
    {
        isbbmonster = false;
        isccmonster = true;

        // ���� ���� ���� �� ����
        PlayerPrefs.SetInt("IsBbMonster", 0);
        PlayerPrefs.SetInt("IsCcMonster", 1);
        PlayerPrefs.Save();
    }
    public void ThirdMonsterDead()
    {
        isccmonster = false;
        // ���� ���� ���� �� ����
        PlayerPrefs.SetInt("IsCcMonster", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("FourClear", 1);
        PlayerPrefs.Save();
        // ��� OneClearLand ��ũ��Ʈ�� ���� ������Ʈ ã��
        FourClearLand[] fourClearLands = FindObjectsOfType<FourClearLand>();

        // ��� OneClearLand ������Ʈ�� ���� �޼��� ȣ��
        foreach (FourClearLand fourClearLand in fourClearLands)
        {
            fourClearLand.ChangeSprite();
        }
    }


    private IEnumerator SpawnMonstersWithDelay(int spawnCount)
    {
        int count = 0; // ������ ���� ��

        while (count < spawnCount)
        {
            if (isaamonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(aamonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // ���� ���� �� ����
                }
            }
            else if (isbbmonster && !isaamonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(bbmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // ���� ���� �� ����
                }
            }
            else if (isccmonster && !isbbmonster && !isaamonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(ccmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // ���� ���� �� ����
                }
            }
            yield return new WaitForSeconds(Random.Range(5f, 7f));
        }
    }
}
