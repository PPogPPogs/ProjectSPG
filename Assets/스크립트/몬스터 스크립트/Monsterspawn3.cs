using UnityEngine;

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
        if (playerInsideTrigger)
        {
            timer += Time.deltaTime;

            if (timer >= spawnDelay)
            {
                SpawnMonsters();
                timer = 0f;
                monstersSpawned = true; // 몬스터가 한 번만 생성되도록 플래그 설정
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
    }

    private void SpawnMonsters()
    {
        if (isamonster)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(amonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else if (isbmonster && !isamonster)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(bmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else if (iscmonster && !isbmonster && !isamonster)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(cmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
