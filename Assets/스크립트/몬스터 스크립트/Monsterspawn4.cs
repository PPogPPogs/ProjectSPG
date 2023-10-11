using UnityEngine;

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
        if (playerInsideTrigger)
        {
            timer += Time.deltaTime;

            if (timer >= spawnDelay)
            {
                SpawnMonsters();
                timer = 0f;
                monstersSpawned = true; // ���Ͱ� �� ���� �����ǵ��� �÷��� ����
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
