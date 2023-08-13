using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPortal : MonoBehaviour
{
    public GameObject monsterPrefab; // ������ ���� ������
    public GameObject intermediateMonsterPrefab; // ������ �߰� ���� ������
    public GameObject bossMonsterPrefab; // ������ ���� ���� ������
    public Transform spawnPoint; // ���� ���� ��ġ

    private int normalMonstersKilled = 0; // ���� �Ϲ� ���� ��
    private int intermediateMonstersKilled = 0; // ���� �߰� ���� ��
    private bool isIntermediateSpawned = false; // �߰� ���Ͱ� �̹� �����Ǿ����� ����
    private bool isBossSpawned = false; // ���� ���Ͱ� �̹� �����Ǿ����� ����

    private float nextMonsterSpawnTime; // ���� ���� ���� �ð�
    public float minSpawnInterval = 2.0f; // �ּ� ���� ���� (��)
    public float maxSpawnInterval = 5.0f; // �ִ� ���� ���� (��)

    private void Start()
    {
        nextMonsterSpawnTime = GetRandomSpawnTime();
    }

    private void Update()
    {
        CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
        if (calendarManager != null)
        {
            int currentHour = calendarManager.GetHour();
            int currentDay = calendarManager.GetDay();

            if (currentDay % 2 == 0 && currentHour >= 1)
            {
                if (!isBossSpawned)
                {
                    if (!isIntermediateSpawned && intermediateMonstersKilled >= 50)
                    {
                        SpawnIntermediateMonster();
                        isIntermediateSpawned = true;
                    }
                    else if (!isIntermediateSpawned)
                    {
                        if (Time.time >= nextMonsterSpawnTime)
                        {
                            SpawnMonster();
                            nextMonsterSpawnTime = Time.time + GetRandomSpawnTime();
                        }
                    }
                    else
                    {
                        if (normalMonstersKilled >= 50)
                        {
                            SpawnBossMonster();
                            isBossSpawned = true;
                        }
                    }
                }
            }
        }
    }

    private float GetRandomSpawnTime()
    {
        return Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    private void SpawnMonster()
    {
        Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void SpawnIntermediateMonster()
    {
        Instantiate(intermediateMonsterPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void SpawnBossMonster()
    {
        Instantiate(bossMonsterPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void MonsterKilled()
    {
        normalMonstersKilled++;

        // ���� ���� ���� ���� �߰� ���� ���� ���� Ȯ��
        if (!isIntermediateSpawned && normalMonstersKilled >= 50)
        {
            SpawnIntermediateMonster();
            isIntermediateSpawned = true;
        }

        // ���� ���� ���� ���� ���� ���� ���� ���� Ȯ��
        if (!isBossSpawned && intermediateMonstersKilled >= 50)
        {
            SpawnBossMonster();
            isBossSpawned = true;
        }
    }
}
