using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class MonsterListSave : MonoBehaviour
{
    [SerializeField] private GameObject Monster1Prefab;

    private string savePath = "path/to/save/file_monster.json";

    private List<SerializableVector3> monsterPositions = new List<SerializableVector3>();

    private void Awake()
    {
        LoadAllMonsterPositions();
    }

    private void LoadAllMonsterPositions()
    {
        List<SerializableVector3> savedPositions = GetSavedmonsterPositions();

        foreach (SerializableVector3 position in savedPositions)
        {
            InstantiateMonster(position.ToVector3());
            Debug.Log("��ġ����");
        }
    }

    private List<SerializableVector3> GetSavedmonsterPositions()
    {
        List<SerializableVector3> savedPositions = new List<SerializableVector3>();

        string json = File.ReadAllText(savePath);
        savedPositions = JsonConvert.DeserializeObject<List<SerializableVector3>>(json);

        return savedPositions;
    }

    private void InstantiateMonster(Vector3 position)
    {
        GameObject monsterObject = Instantiate(Monster1Prefab, position, Quaternion.identity);
        // Godon ��ũ��Ʈ�� ���� �߰� �۾��� �ʿ��� �� �ֽ��ϴ�.
    }

    public void SaveGodonPositions()
    {
        monsterPositions.Clear();

        // ���� Godon ��ġ ������ ����Ʈ�� ����
        // godonList ��� ����ȭ�� ����Ʈ�� ����մϴ�.
        foreach (GameObject monsterObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            MonsterAI monsterAI = monsterObject.GetComponent<MonsterAI>();
            Vector3 position = monsterAI.LoadGodonPosition();
            monsterPositions.Add(new SerializableVector3(position));
            Debug.Log("����?");
        }

        // ����Ʈ�� JSON���� ��ȯ�Ͽ� ���Ͽ� ����
        string json = JsonConvert.SerializeObject(monsterPositions);
        File.WriteAllText(savePath, json);
        Debug.Log("����");
    }

    private void OnApplicationQuit()
    {
        SaveGodonPositions();
    }
}