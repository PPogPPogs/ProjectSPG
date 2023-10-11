using UnityEngine;

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
    }

    private void SpawnMonsters()
    {
        if (isonemonster)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(onemonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else if (istwomonster && !isonemonster)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(twomonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else if (isthreemonster && !istwomonster && !isonemonster)
        {
            foreach (Transform spawnPoint in spawnPoints)
            {
                Instantiate(threemonsterPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
