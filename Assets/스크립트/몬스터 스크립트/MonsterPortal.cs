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
    private Animator portalAnimator;
    private bool isBossDie = false;

    private float nextMonsterSpawnTime; // 다음 몬스터 생성 시간
    public float minSpawnInterval = 2.0f; // 최소 생성 간격 (초)
    public float maxSpawnInterval = 5.0f; // 최대 생성 간격 (초)
    private bool isPortalOpen = false;

    private float portalUpdateInterval = 1.0f; // 포털 업데이트 간격 (초)
    private float timeSinceLastPortalUpdate = 0.0f;

    private void Start()
    {
        nextMonsterSpawnTime = GetRandomSpawnTime();
        portalAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdatePortalStatus();

        CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
        if (calendarManager != null && isPortalOpen)
        {
            int currentHour = calendarManager.GetHour();
            int currentDay = calendarManager.GetDay();
            if (currentDay % 2 == 0)
            {
                if (!isBossSpawned)
                {
                    if (!isIntermediateSpawned && normalMonstersKilled >= 5)
                    {
                        SpawnIntermediateMonster();
                        isIntermediateSpawned = true;
                    }
                    else if (isIntermediateSpawned && normalMonstersKilled <= 10 && Time.time >= nextMonsterSpawnTime)
                    {
                        SpawnIntermediateMonster();
                        isIntermediateSpawned = true;
                        nextMonsterSpawnTime = Time.time + GetRandomSpawnTime();
                    }
                    else if (isIntermediateSpawned && normalMonstersKilled >= 11)
                    {
                        SpawnBossMonster();
                        isBossSpawned = true;
                    }
                    else if (!isIntermediateSpawned && Time.time >= nextMonsterSpawnTime)
                    {
                        SpawnMonster();
                        nextMonsterSpawnTime = Time.time + GetRandomSpawnTime();
                    }
                }
                portalAnimator.SetBool("IsOpen", true);
                if (isBossSpawned)
                {
                    if (isBossDie == true)
                    {
                        portalAnimator.SetBool("IsOpen", false);
                    }
                }
            }

            else
            {
                // 애니메이션 재생: 포털 닫힘
                portalAnimator.SetBool("IsOpen", false);
            }
        }
    } 

    private void UpdatePortalStatus()
    {
        timeSinceLastPortalUpdate += Time.deltaTime;

        if (timeSinceLastPortalUpdate >= portalUpdateInterval)
        {
            CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
            if (calendarManager != null)
            {
                int currentHour = calendarManager.GetHour();
                int currentDay = calendarManager.GetDay();

                if (currentHour >= 1)
                {
                    isPortalOpen = true;
                }
                else
                {
                    isPortalOpen = false; // 3시 이전에는 포탈 닫기
                }
            }

            timeSinceLastPortalUpdate = 0.0f; // 간격 초기화
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

    public void ClosePortal()
    {
            isBossDie = true;
            Update();
    } 


    public void MonsterKilled()
    {
        normalMonstersKilled++;

        if (!isIntermediateSpawned && normalMonstersKilled >= 5)
        {
            SpawnIntermediateMonster();
            isIntermediateSpawned = true;
        }

        if (!isBossSpawned && intermediateMonstersKilled >= 5)
        {
            SpawnBossMonster();
            isBossSpawned = true;
        }
    }
}
