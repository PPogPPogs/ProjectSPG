using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class MonsterListSave : MonoBehaviour
{
    [SerializeField] private GameObject Monster1Prefab;

    private string savePath = "path/to/save/file_monster.json";

    // ����: ��ġ�� ü���� �Բ� ������ ����ȭ�� Ŭ����
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
            Debug.Log("��ġ �� ü�� ����");
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

        // ����: MonsterHealth ��ũ��Ʈ�� ü�� ���� ����
        MonsterHealth monsterHealth = monsterObject.GetComponent<MonsterHealth>();
        if (monsterHealth != null)
        {
            monsterHealth.SetHealth(health);
        }
    }

    public void SaveMonsterDataList()
    {
        monsterDataList.Clear();

        // ���� Monster ��ġ�� ü�� ������ ����Ʈ�� ����
        foreach (GameObject monsterObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            MonsterAI monsterAI = monsterObject.GetComponent<MonsterAI>();
            MonsterHealth monsterHealth = monsterObject.GetComponent<MonsterHealth>();

            Vector3 position = monsterAI.LoadGodonPosition();
            int health = monsterHealth.GetCurrentHealth();

            monsterDataList.Add(new SerializableMonsterData(position, health));
            Debug.Log("��ġ �� ü�� ����");
        }

        // ����Ʈ�� JSON���� ��ȯ�Ͽ� ���Ͽ� ����
        string json = JsonConvert.SerializeObject(monsterDataList);
        File.WriteAllText(savePath, json);
        Debug.Log("����");
    }

    private void OnApplicationQuit()
    {
        SaveMonsterDataList();
    }
}
