using UnityEngine;
using System.Collections;
public class MonsterSpawn3 : MonoBehaviour
{
    // ���� ������ �� ���� ��ġ ����

    public GameObject amonsterPrefab;
    public GameObject bmonsterPrefab;
    public GameObject cmonsterPrefab;
    private bool playerInsideTrigger = false;
    private float spawnDelay = 10f; // ���� ���� ��� �ð�
    private float timer = 0f;
    private bool monstersSpawned = false;
    private bool monsterSSpawned = false;
    private bool isamonster = true;
    private bool isbmonster = false;
    private bool iscmonster = false;
    public Transform[] spawnPoints; // ���� ���� ��ġ �迭

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !monstersSpawned)
        {
            playerInsideTrigger = true;

            // Ʈ���� ���� �� ���� ���� ����
            PlayerPrefs.SetInt("IsAMonster", isamonster ? 1 : 0);
            PlayerPrefs.SetInt("IsBwoMonster", isbmonster ? 1 : 0);
            PlayerPrefs.SetInt("IsCMonster", iscmonster ? 1 : 0);
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
        isamonster = PlayerPrefs.GetInt("IsAMonster", 1) == 1;
        isbmonster = PlayerPrefs.GetInt("IsBMonster", 0) == 1;
        iscmonster = PlayerPrefs.GetInt("IsCMonster", 0) == 1;
    }

    private void Update()
    {
        CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
        if (calendarManager != null)
        {
            int currentHour = calendarManager.GetHour();
            int currentDay = calendarManager.GetDay();
            if (currentDay == 29 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(3));

                monsterSSpawned = true;

            }

            else if (currentDay == 30 && currentHour == 7 && !monsterSSpawned)
            {
                FirstMonsterDead();
            }

            else if (currentDay == 30 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(3));
                monsterSSpawned = true;
            }

            else if (currentDay == 31 && currentHour == 7 && !monsterSSpawned)
            {
                SecondMonsterDead();
            }

            else if (currentDay == 31 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(3));
                monsterSSpawned = true;
            }

            else if (currentDay == 32 && currentHour == 7 && !monsterSSpawned)
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
        isamonster = false;
        isbmonster = true;

        // ���� ���� ���� �� ����
        PlayerPrefs.SetInt("IsAMonster", 0);
        PlayerPrefs.SetInt("IsBMonster", 1);
        PlayerPrefs.Save();
    }

    public void SecondMonsterDead()
    {
        isbmonster = false;
        iscmonster = true;

        // ���� ���� ���� �� ����
        PlayerPrefs.SetInt("IsBMonster", 0);
        PlayerPrefs.SetInt("IsCMonster", 1);
        PlayerPrefs.Save();
    }


    public void ThirdMonsterDead()
    {
        iscmonster = false;
        // ���� ���� ���� �� ����
        PlayerPrefs.SetInt("IsCMonster", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("ThreeClear", 1);
        PlayerPrefs.Save();
        // ��� OneClearLand ��ũ��Ʈ�� ���� ������Ʈ ã��
        ThreeClearLand[] threeClearLands = FindObjectsOfType<ThreeClearLand>();

        // ��� OneClearLand ������Ʈ�� ���� �޼��� ȣ��
        foreach (ThreeClearLand threeClearLand in threeClearLands)
        {
            threeClearLand.ChangeSprite();
        }
    }

   
    private IEnumerator SpawnMonstersWithDelay(int spawnCount)
    {
        int count = 0; // ������ ���� ��

        while (count < spawnCount)
        {
            if (isamonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(amonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // ���� ���� �� ����
                }
            }
            else if (isbmonster && !isamonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(bmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // ���� ���� �� ����
                }
            }
            else if (iscmonster && !isbmonster && !isamonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(cmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // ���� ���� �� ����
                }
            }
            yield return new WaitForSeconds(Random.Range(5f, 7f));
        }
    }
}
