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
    private Animator portalAnimator;
    private bool isBossDie = false;

    private float nextMonsterSpawnTime; // ���� ���� ���� �ð�
    public float minSpawnInterval = 2.0f; // �ּ� ���� ���� (��)
    public float maxSpawnInterval = 5.0f; // �ִ� ���� ���� (��)
    private bool isPortalOpen = false;

    private float portalUpdateInterval = 1.0f; // ���� ������Ʈ ���� (��)
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
                // �ִϸ��̼� ���: ���� ����
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
                    isPortalOpen = false; // 3�� �������� ��Ż �ݱ�
                }
            }

            timeSinceLastPortalUpdate = 0.0f; // ���� �ʱ�ȭ
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
