using UnityEngine;
using System.Collections;
public class MonsterSpawn2 : MonoBehaviour
{
    // 몬스터 프리팹 및 스폰 위치 설정

    public GameObject onemonsterPrefab;
    public GameObject twomonsterPrefab;
    public GameObject threemonsterPrefab;
    private bool playerInsideTrigger = false;
    private float spawnDelay = 10f; // 몬스터 생성 대기 시간
    private float timer = 0f;
    private bool monstersSpawned = false;
    private bool monsterSSpawned = false;
    private bool isonemonster = true;
    private bool istwomonster = false;
    private bool isthreemonster = false;
    public Transform[] spawnPoints; // 몬스터 스폰 위치 배열

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !monstersSpawned)
        {
            playerInsideTrigger = true;

            // 트리거 진입 시 몬스터 상태 저장
            PlayerPrefs.SetInt("IsOneMonster", isonemonster ? 1 : 0);
            PlayerPrefs.SetInt("IsTwoMonster", istwomonster ? 1 : 0);
            PlayerPrefs.SetInt("IsThreeMonster", isthreemonster ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = false;
            timer = 0f;
        }
    }

    private void Start()
    {
        // 게임 시작 시 저장된 몬스터 상태 불러오기
        isonemonster = PlayerPrefs.GetInt("IsOneMonster", 1) == 1;
        istwomonster = PlayerPrefs.GetInt("IsTwoMonster", 0) == 1;
        isthreemonster = PlayerPrefs.GetInt("IsThreeMonster", 0) == 1;
    }

    private void Update()
    {
        CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
        if (calendarManager != null)
        {
            int currentHour = calendarManager.GetHour();
            int currentDay = calendarManager.GetDay();
            if (currentDay == 26 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(3));

                monsterSSpawned = true;

            }

            else if (currentDay == 27 && currentHour == 7 && !monsterSSpawned)
            {
                FirstMonsterDead();
            }

            else if (currentDay == 27 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(3));
                monsterSSpawned = true;
            }

            else if (currentDay == 28 && currentHour == 7 && !monsterSSpawned)
            {
                SecondMonsterDead();
            }

            else if (currentDay == 28 && currentHour == 19 && !monsterSSpawned)
            {
                StartCoroutine(SpawnMonstersWithDelay(3));
                monsterSSpawned = true;
            }

            else if (currentDay == 29 && currentHour == 7 && !monsterSSpawned)
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
        isonemonster = false;
        istwomonster = true;

        // 몬스터 상태 변경 후 저장
        PlayerPrefs.SetInt("IsOneMonster", 0);
        PlayerPrefs.SetInt("IsTwoMonster", 1);
        PlayerPrefs.Save();
    }

    public void SecondMonsterDead()
    {
        istwomonster = false;
        isthreemonster = true;

        // 몬스터 상태 변경 후 저장
        PlayerPrefs.SetInt("IsTwoMonster", 0);
        PlayerPrefs.SetInt("IsThreeMonster", 1);
        PlayerPrefs.Save();
    }

    public void ThirdMonsterDead()
    {
        isthreemonster = false;
        // 몬스터 상태 변경 후 저장
        PlayerPrefs.SetInt("IsThreeMonster", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("TwoClear", 1);
        PlayerPrefs.Save();
        // 모든 OneClearLand 스크립트를 가진 오브젝트 찾기
        TwoClearLand[] twoClearLands = FindObjectsOfType<TwoClearLand>();

        // 모든 OneClearLand 오브젝트에 대해 메서드 호출
        foreach (TwoClearLand twoClearLand in twoClearLands)
        {
            twoClearLand.ChangeSprite();
        }
    }

    private IEnumerator SpawnMonstersWithDelay(int spawnCount)
    {
        int count = 0; // 생성된 몬스터 수

        while (count < spawnCount)
        {
            if (isonemonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(onemonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // 몬스터 생성 수 증가
                }
            }
            else if (istwomonster && !isonemonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(twomonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // 몬스터 생성 수 증가
                }
            }
            else if (isthreemonster && !istwomonster && !isonemonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(threemonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // 몬스터 생성 수 증가
                }
            }
            yield return new WaitForSeconds(Random.Range(5f, 7f));
        }
    }
}
