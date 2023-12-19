using UnityEngine;
using System.Collections;

public class MonsterSpawn1 : MonoBehaviour
{
    // ���� ������ �� ���� ��ġ ����

    public GameObject firstmonsterPrefab;
    public GameObject secondmonsterPrefab;
    public GameObject thirdmonsterPrefab;
    private bool playerInsideTrigger = false;
    private float spawnDelay = 10f; // ���� ���� ��� �ð�
    private float timer = 0f;
    private bool monstersSpawned = false;
    private bool monsterSSpawned = false;
    private bool isfirstmonster = true;
    private bool issecondmonster = false;
    private bool isthirdmonster = false;
    public Transform[] spawnPoints; // ���� ���� ��ġ �迭
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !monstersSpawned)
        {
            playerInsideTrigger = true;

            // Ʈ���� ���� �� ���� ���� ����
            PlayerPrefs.SetInt("IsFirstMonster", isfirstmonster ? 1 : 0);
            PlayerPrefs.SetInt("IsSecondMonster", issecondmonster ? 1 : 0);
            PlayerPrefs.SetInt("IsThirdMonster", isthirdmonster ? 1 : 0);
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
        isfirstmonster = PlayerPrefs.GetInt("IsFirstMonster", 1) == 1;
        issecondmonster = PlayerPrefs.GetInt("IsSecondMonster", 0) == 1;
        isthirdmonster = PlayerPrefs.GetInt("IsThirdMonster", 0) == 1;
      

       

    }

    private void Update()
    {
        CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
        if (calendarManager != null)
        {
            int currentHour = calendarManager.GetHour();
            int currentDay = calendarManager.GetDay();
            if (currentDay == 23 && currentHour == 19 && !monsterSSpawned )
            {
                StartCoroutine(SpawnMonstersWithDelay(15));

                monsterSSpawned = true;
                
            }

            else if (currentDay == 24 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(30));
                monsterSSpawned = true;
            }

            else if (currentDay == 25 && currentHour == 7 && !monsterSSpawned)
            {
                FirstMonsterDead();
                int valueToSave = 1;

                // "BuildCannonAndWall" Ű�� ���� �����մϴ�.
                PlayerPrefs.SetInt("BuildCannonAndWall", valueToSave);

                // ��������� �����մϴ�.
                PlayerPrefs.Save();
                

            }

            if (currentDay == 25 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(15));

                monsterSSpawned = true;

            }

            else if (currentDay == 26 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(30));
                monsterSSpawned = true;
            }

            else if (currentDay == 27 && currentHour == 7 && !monsterSSpawned)
            {
                SecondMonsterDead();
                int valueToSave = 2;

                // "BuildCannonAndWall" Ű�� ���� �����մϴ�.
                PlayerPrefs.SetInt("BuildCannonAndWall", valueToSave);

                // ��������� �����մϴ�.
                PlayerPrefs.Save();
            }

            if (currentDay == 27 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(15));

                monsterSSpawned = true;

            }

            else if (currentDay == 28 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(30));
                monsterSSpawned = true;
            }

            else if (currentDay == 29 && currentHour == 7 && !monsterSSpawned)
            {
                ThirdMonsterDead();
                int valueToSave = 3;

                // "BuildCannonAndWall" Ű�� ���� �����մϴ�.
                PlayerPrefs.SetInt("BuildCannonAndWall", valueToSave);

                // ��������� �����մϴ�.
                PlayerPrefs.Save();
            }

            if (currentHour == 20)
            {
                monsterSSpawned = false;
            }

            
        }

    }

    public void FirstMonsterDead()
    {
        isfirstmonster = false;
        issecondmonster = true;

        // ���� ���� ���� �� ����
        PlayerPrefs.SetInt("IsFirstMonster", 0);
        PlayerPrefs.SetInt("IsSecondMonster", 1);
        PlayerPrefs.Save();
    }

    public void SecondMonsterDead()
    {
        issecondmonster = false;
        isthirdmonster = true;

        // ���� ���� ���� �� ����
        PlayerPrefs.SetInt("IsSecondMonster", 0);
        PlayerPrefs.SetInt("IsThirdMonster", 1);
        PlayerPrefs.Save();
    }

    public void ThirdMonsterDead()
    {
        isthirdmonster = false;
        // ���� ���� ���� �� ����
        PlayerPrefs.SetInt("IsThirdMonster", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("OneClear", 1);
        PlayerPrefs.Save();
        // ��� OneClearLand ��ũ��Ʈ�� ���� ������Ʈ ã��
        OneClearLand[] oneClearLands = FindObjectsOfType<OneClearLand>();

        // ��� OneClearLand ������Ʈ�� ���� �޼��� ȣ��
        foreach (OneClearLand oneClearLand in oneClearLands)
        {
            oneClearLand.ChangeSprite();
        }
    }

    private void SpawnMonsters()
    {
        if (isfirstmonster)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(firstmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else if (issecondmonster && !isfirstmonster)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(secondmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else if (isthirdmonster && !issecondmonster && !isfirstmonster)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(thirdmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }

    private IEnumerator SpawnMonstersWithDelay(int spawnCount)
    {
        int count = 0; // ������ ���� ��

        while (count < spawnCount)
        {
            if (isfirstmonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(firstmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // ���� ���� �� ����
                }
            }
            else if (issecondmonster && !isfirstmonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(secondmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // ���� ���� �� ����
                }
            }
            else if (isthirdmonster && !issecondmonster && !isfirstmonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(thirdmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // ���� ���� �� ����
                }
            }
            yield return new WaitForSeconds(Random.Range(5f, 7f));
        }
    }



}
