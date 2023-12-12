using UnityEngine;
using System.Collections.Generic;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // ���� ������
    public Transform[] spawnPoints; // ���� ����Ʈ �迭
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
        // 5���� ���� ����Ʈ �߿��� 3���� �������� ����
        List<Transform> selectedSpawnPoints = SelectRandomSpawnPoints(3);

        // ���õ� ���� ����Ʈ�� ���� ����
        foreach (Transform spawnPoint in selectedSpawnPoints)
        {
            Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
            
        }
       
    }

    List<Transform> SelectRandomSpawnPoints(int count)
    {
        List<Transform> shuffledSpawnPoints = new List<Transform>(spawnPoints);
        ShuffleList(shuffledSpawnPoints); // ����Ʈ�� �����ϴ�.

        List<Transform> selectedSpawnPoints = new List<Transform>();

        // ���ϴ� ������ŭ ���� ����Ʈ�� �����մϴ�.
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
