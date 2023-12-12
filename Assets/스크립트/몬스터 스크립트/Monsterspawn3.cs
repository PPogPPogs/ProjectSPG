using UnityEngine;
using System.Collections;
public class MonsterSpawn3 : MonoBehaviour
{
    // 몬스터 프리팹 및 스폰 위치 설정

    public GameObject amonsterPrefab;
    public GameObject bmonsterPrefab;
    public GameObject cmonsterPrefab;
    private bool playerInsideTrigger = false;
    private float spawnDelay = 10f; // 몬스터 생성 대기 시간
    private float timer = 0f;
    private bool monstersSpawned = false;
    private bool monsterSSpawned = false;
    private bool isamonster = true;
    private bool isbmonster = false;
    private bool iscmonster = false;
    public Transform[] spawnPoints; // 몬스터 스폰 위치 배열

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !monstersSpawned)
        {
            playerInsideTrigger = true;

            // 트리거 진입 시 몬스터 상태 저장
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
        // 게임 시작 시 저장된 몬스터 상태 불러오기
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

        // 몬스터 상태 변경 후 저장
        PlayerPrefs.SetInt("IsAMonster", 0);
        PlayerPrefs.SetInt("IsBMonster", 1);
        PlayerPrefs.Save();
    }

    public void SecondMonsterDead()
    {
        isbmonster = false;
        iscmonster = true;

        // 몬스터 상태 변경 후 저장
        PlayerPrefs.SetInt("IsBMonster", 0);
        PlayerPrefs.SetInt("IsCMonster", 1);
        PlayerPrefs.Save();
    }


    public void ThirdMonsterDead()
    {
        iscmonster = false;
        // 몬스터 상태 변경 후 저장
        PlayerPrefs.SetInt("IsCMonster", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("ThreeClear", 1);
        PlayerPrefs.Save();
        // 모든 OneClearLand 스크립트를 가진 오브젝트 찾기
        ThreeClearLand[] threeClearLands = FindObjectsOfType<ThreeClearLand>();

        // 모든 OneClearLand 오브젝트에 대해 메서드 호출
        foreach (ThreeClearLand threeClearLand in threeClearLands)
        {
            threeClearLand.ChangeSprite();
        }
    }

   
    private IEnumerator SpawnMonstersWithDelay(int spawnCount)
    {
        int count = 0; // 생성된 몬스터 수

        while (count < spawnCount)
        {
            if (isamonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(amonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // 몬스터 생성 수 증가
                }
            }
            else if (isbmonster && !isamonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(bmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // 몬스터 생성 수 증가
                }
            }
            else if (iscmonster && !isbmonster && !isamonster)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Instantiate(cmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    count++; // 몬스터 생성 수 증가
                }
            }
            yield return new WaitForSeconds(Random.Range(5f, 7f));
        }
    }
}
