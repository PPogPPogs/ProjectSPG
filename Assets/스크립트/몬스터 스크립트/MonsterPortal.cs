using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPortal : MonoBehaviour
{
    public GameObject monsterPrefab; // 생성할 몬스터 프리팹
    public GameObject intermediateMonsterPrefab; // 생성할 중간 몬스터 프리팹
    public GameObject bossMonsterPrefab; // 생성할 보스 몬스터 프리팹
    public Transform spawnPoint; // 몬스터 생성 위치

    private int normalMonstersKilled = 0; // 죽인 일반 몬스터 수
    private int intermediateMonstersKilled = 0; // 죽인 중간 몬스터 수
    private bool isIntermediateSpawned = false; // 중간 몬스터가 이미 생성되었는지 여부
    private bool isBossSpawned = false; // 보스 몬스터가 이미 생성되었는지 여부

    private float nextMonsterSpawnTime; // 다음 몬스터 생성 시간
    public float minSpawnInterval = 2.0f; // 최소 생성 간격 (초)
    public float maxSpawnInterval = 5.0f; // 최대 생성 간격 (초)

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

        // 죽인 몬스터 수에 따라 중간 몬스터 생성 여부 확인
        if (!isIntermediateSpawned && normalMonstersKilled >= 50)
        {
            SpawnIntermediateMonster();
            isIntermediateSpawned = true;
        }

        // 죽인 몬스터 수에 따라 보스 몬스터 생성 여부 확인
        if (!isBossSpawned && intermediateMonstersKilled >= 50)
        {
            SpawnBossMonster();
            isBossSpawned = true;
        }
    }
}
