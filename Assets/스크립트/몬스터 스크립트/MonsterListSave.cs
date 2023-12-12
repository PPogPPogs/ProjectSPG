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
            Debug.Log("위치전달");
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
        // Godon 스크립트에 대한 추가 작업이 필요할 수 있습니다.
    }

    public void SaveGodonPositions()
    {
        monsterPositions.Clear();

        // 현재 Godon 위치 정보를 리스트에 저장
        // godonList 대신 직렬화된 리스트를 사용합니다.
        foreach (GameObject monsterObject in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            MonsterAI monsterAI = monsterObject.GetComponent<MonsterAI>();
            Vector3 position = monsterAI.LoadGodonPosition();
            monsterPositions.Add(new SerializableVector3(position));
            Debug.Log("저장?");
        }

        // 리스트를 JSON으로 변환하여 파일에 저장
        string json = JsonConvert.SerializeObject(monsterPositions);
        File.WriteAllText(savePath, json);
        Debug.Log("저장");
    }

    private void OnApplicationQuit()
    {
        SaveGodonPositions();
    }
}