using UnityEngine;

public class MonsterSpawn2 : MonoBehaviour
{
    // ���� ������ �� ���� ��ġ ����

    public GameObject onemonsterPrefab;
    public GameObject twomonsterPrefab;
    public GameObject threemonsterPrefab;
    private bool playerInsideTrigger = false;
    private float spawnDelay = 10f; // ���� ���� ��� �ð�
    private float timer = 0f;
    private bool monstersSpawned = false;

    private bool isonemonster = true;
    private bool istwomonster = false;
    private bool isthreemonster = false;
    public Transform[] spawnPoints; // ���� ���� ��ġ �迭

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !monstersSpawned)
        {
            playerInsideTrigger = true;

            // Ʈ���� ���� �� ���� ���� ����
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
        // ���� ���� �� ����� ���� ���� �ҷ�����
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
                monstersSpawned = true; // ���Ͱ� �� ���� �����ǵ��� �÷��� ����
            }
        }
    }

    public void FirstMonsterDead()
    {
        isonemonster = false;
        istwomonster = true;

        // ���� ���� ���� �� ����
        PlayerPrefs.SetInt("IsOneMonster", 0);
        PlayerPrefs.SetInt("IsTwoMonster", 1);
        PlayerPrefs.Save();
    }

    public void SecondMonsterDead()
    {
        istwomonster = false;
        isthreemonster = true;

        // ���� ���� ���� �� ����
        PlayerPrefs.SetInt("IsTwoMonster", 0);
        PlayerPrefs.SetInt("IsThreeMonster", 1);
        PlayerPrefs.Save();
    }

    public void ThirdMonsterDead()
    {
        isthreemonster = false;

        // ���� ���� ���� �� ����
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
