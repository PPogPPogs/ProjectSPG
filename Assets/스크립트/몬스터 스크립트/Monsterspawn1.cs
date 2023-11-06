using UnityEngine;
using System.Collections;

public class MonsterSpawn1 : MonoBehaviour
{
    // 몬스터 프리팹 및 스폰 위치 설정

    public GameObject firstmonsterPrefab;
    public GameObject secondmonsterPrefab;
    public GameObject thirdmonsterPrefab;
    private bool playerInsideTrigger = false;
    private float spawnDelay = 10f; // 몬스터 생성 대기 시간
    private float timer = 0f;
    private bool monstersSpawned = false;
    private bool monsterSSpawned = false;
    private bool isfirstmonster = true;
    private bool issecondmonster = false;
    private bool isthirdmonster = false;
    public Transform[] spawnPoints; // 몬스터 스폰 위치 배열
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !monstersSpawned)
        {
            playerInsideTrigger = true;

            // 트리거 진입 시 몬스터 상태 저장
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
        // 게임 시작 시 저장된 몬스터 상태 불러오기
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
            if (currentHour == 18 && !monsterSSpawned )
            {
                StartCoroutine(SpawnMonstersWithDelay(30));

                monsterSSpawned = true;
            }
            else if (currentHour == 19)
            {
                monsterSSpawned = false;
            }
        }

    }

    public void FirstMonsterDead()
    {
        isfirstmonster = false;
        issecondmonster = true;

        // 몬스터 상태 변경 후 저장
        PlayerPrefs.SetInt("IsFirstMonster", 0);
        PlayerPrefs.SetInt("IsSecondMonster", 1);
        PlayerPrefs.Save();
    }

    public void SecondMonsterDead()
    {
        issecondmonster = false;
        isthirdmonster = true;

        // 몬스터 상태 변경 후 저장
        PlayerPrefs.SetInt("IsSecondMonster", 0);
        PlayerPrefs.SetInt("IsThirdMonster", 1);
        PlayerPrefs.Save();
    }

    public void ThirdMonsterDead()
    {
        isthirdmonster = false;

        // 몬스터 상태 변경 후 저장
        PlayerPrefs.SetInt("IsThirdMonster", 0);
        PlayerPrefs.Save();
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
        int count = 0; // 생성된 몬스터 수

        while (count < spawnCount)
        {
            if (isfirstmonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(firstmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // 몬스터 생성 수 증가
                }
            }
            else if (issecondmonster && !isfirstmonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(secondmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // 몬스터 생성 수 증가
                }
            }
            else if (isthirdmonster && !issecondmonster && !isfirstmonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(thirdmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // 몬스터 생성 수 증가
                }
            }
            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }
    }



}
