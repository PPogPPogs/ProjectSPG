using UnityEngine;

public class MonsterSpawn4 : MonoBehaviour
{
    // 몬스터 프리팹 및 스폰 위치 설정

    public GameObject aamonsterPrefab;
    public GameObject bbmonsterPrefab;
    public GameObject ccmonsterPrefab;
    private bool playerInsideTrigger = false;
    private float spawnDelay = 10f; // 몬스터 생성 대기 시간
    private float timer = 0f;
    private bool monstersSpawned = false;

    private bool isaamonster = true;
    private bool isbbmonster = false;
    private bool isccmonster = false;
    public Transform[] spawnPoints; // 몬스터 스폰 위치 배열

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !monstersSpawned)
        {
            playerInsideTrigger = true;

            // 트리거 진입 시 몬스터 상태 저장
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
        }
    }

    private void Start()
    {
        // 게임 시작 시 저장된 몬스터 상태 불러오기
        isaamonster = PlayerPrefs.GetInt("IsAaMonster", 1) == 1;
        isbbmonster = PlayerPrefs.GetInt("IsBbMonster", 0) == 1;
        isccmonster = PlayerPrefs.GetInt("IsCcMonster", 0) == 1;
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
        isaamonster = false;
        isbbmonster = true;

        // 몬스터 상태 변경 후 저장
        PlayerPrefs.SetInt("IsAaMonster", 0);
        PlayerPrefs.SetInt("IsBbMonster", 1);
        PlayerPrefs.Save();
    }

    public void SecondMonsterDead()
    {
        isbbmonster = false;
        isccmonster = true;

        // 몬스터 상태 변경 후 저장
        PlayerPrefs.SetInt("IsBbMonster", 0);
        PlayerPrefs.SetInt("IsCcMonster", 1);
        PlayerPrefs.Save();
    }

    public void ThirdMonsterDead()
    {
        isccmonster = false;

        // 몬스터 상태 변경 후 저장
        PlayerPrefs.SetInt("IsCcMonster", 0);
        PlayerPrefs.Save();
    }

    private void SpawnMonsters()
    {
        if (isaamonster)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(aamonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else if (isbbmonster && !isaamonster)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(bbmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else if (isccmonster && !isbbmonster && !isaamonster)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(ccmonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
