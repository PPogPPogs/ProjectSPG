using UnityEngine;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // 몬스터 프리팹
    public Transform[] spawnPoints; // 스폰 포인트 배열
    private bool isRespawn = false;

    void Update()
    {
        CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
        if (calendarManager != null)
        {
            int currentHour = calendarManager.GetHour();
            int currentMinute = calendarManager.GetMinute();

            if ((currentHour == 7 && currentMinute == 0) || (currentHour == 7 && currentMinute == 30) || (currentHour == 8 && currentMinute == 0))
            {
                if (!isRespawn)
                {
                    SpawnMonsters();
                    isRespawn = true;
                }
                
            }
            else if ((currentHour == 7 && currentMinute == 15) || (currentHour == 7 && currentMinute == 45) || (currentHour == 8 && currentMinute == 15))
            {
                isRespawn = false;
            }
        }

    }
    void SpawnMonsters()
    {
        // 5개의 스폰 포인트 중에서 3개를 무작위로 선택
        List<Transform> selectedSpawnPoints = SelectRandomSpawnPoints(3);

        // 선택된 스폰 포인트에 몬스터 생성
        foreach (Transform spawnPoint in selectedSpawnPoints)
        {
            Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
            
        }
       
    }

    List<Transform> SelectRandomSpawnPoints(int count)
    {
        List<Transform> shuffledSpawnPoints = new List<Transform>(spawnPoints);
        ShuffleList(shuffledSpawnPoints); // 리스트를 섞습니다.

        List<Transform> selectedSpawnPoints = new List<Transform>();

        // 원하는 개수만큼 스폰 포인트를 선택합니다.
        for (int i = 0; i < count; i++)
        {
            selectedSpawnPoints.Add(shuffledSpawnPoints[i]);
        }

        return selectedSpawnPoints;
    }

    void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
