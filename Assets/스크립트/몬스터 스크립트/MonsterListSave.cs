using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class MonsterListSave : MonoBehaviour
{
    [SerializeField] private GameObject Monster1Prefab;

    private string savePath = "path/to/save/file_monster.json";

    // 수정: 위치와 체력을 함께 저장할 직렬화된 클래스
    [System.Serializable]
    private class SerializableMonsterData
    {
        public SerializableVector3 position;
        public int health;

        public SerializableMonsterData(Vector3 position, int health)
        {
            this.position = new SerializableVector3(position);
            this.health = health;
        }
    }

    private List<SerializableMonsterData> monsterDataList = new List<SerializableMonsterData>();

    private void Awake()
    {
        LoadAllMonsterData();
    }

    private void LoadAllMonsterData()
    {
        List<SerializableMonsterData> savedMonsterDataList = GetSavedMonsterDataList();

        foreach (SerializableMonsterData data in savedMonsterDataList)
        {
            InstantiateMonster(data.position.ToVector3(), data.health);
            Debug.Log("위치 및 체력 전달");
        }
    }

    private List<SerializableMonsterData> GetSavedMonsterDataList()
    {
        List<SerializableMonsterData> savedMonsterDataList = new List<SerializableMonsterData>();

        string json = File.ReadAllText(savePath);
        savedMonsterDataList = JsonConvert.DeserializeObject<List<SerializableMonsterData>>(json);

        return savedMonsterDataList;
    }

    private void InstantiateMonster(Vector3 position, int health)
    {
        GameObject monsterObject = Instantiate(Monster1Prefab, position, Quaternion.identity);

        // 수정: MonsterHealth 스크립트에 체력 정보 전달
        MonsterHealth monsterHealth = monsterObject.GetComponent<MonsterHealth>();
        if (monsterHealth != null)
        {
            monsterHealth.SetHealth(health);
        }
    }

    public void SaveMonsterDataList()
    {
        monsterDataList.Clear();

        // 현재 Monster 위치와 체력 정보를 리스트에 저장
        foreach (GameObject monsterObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            MonsterAI monsterAI = monsterObject.GetComponent<MonsterAI>();
            MonsterHealth monsterHealth = monsterObject.GetComponent<MonsterHealth>();

            Vector3 position = monsterAI.LoadGodonPosition();
            int health = monsterHealth.GetCurrentHealth();

            monsterDataList.Add(new SerializableMonsterData(position, health));
            Debug.Log("위치 및 체력 저장");
        }

        // 리스트를 JSON으로 변환하여 파일에 저장
        string json = JsonConvert.SerializeObject(monsterDataList);
        File.WriteAllText(savePath, json);
        Debug.Log("저장");
    }

    private void OnApplicationQuit()
    {
        SaveMonsterDataList();
    }
}
