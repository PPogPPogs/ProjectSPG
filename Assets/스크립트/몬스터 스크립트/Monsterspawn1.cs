using UnityEngine;

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
}
