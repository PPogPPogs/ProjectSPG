using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class LeftMonsterListSave : MonoBehaviour
{
    [SerializeField] private GameObject LeftMonsterPrefab;

    private string savePath = "path/to/save/file_Leftmonster.json";

    private List<SerializableVector3> leftmonsterPositions = new List<SerializableVector3>();

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
        GameObject leftmonsterObject = Instantiate(LeftMonsterPrefab, position, Quaternion.identity);
        // Godon ��ũ��Ʈ�� ���� �߰� �۾��� �ʿ��� �� �ֽ��ϴ�.
    }

    public void SaveGodonPositions()
    {
        leftmonsterPositions.Clear();

        // ���� Godon ��ġ ������ ����Ʈ�� ����
        // godonList ��� ����ȭ�� ����Ʈ�� ����մϴ�.
        foreach (GameObject leftmonsterObject in GameObject.FindGameObjectsWithTag("LeftMonster"))
        {
            LeftMonster leftMonster = leftmonsterObject.GetComponent<LeftMonster>();
            Vector3 position = leftMonster.LoadLeftmonsterPosition();
            leftmonsterPositions.Add(new SerializableVector3(position));
            Debug.Log("����?");
        }

        // ����Ʈ�� JSON���� ��ȯ�Ͽ� ���Ͽ� ����
        string json = JsonConvert.SerializeObject(leftmonsterPositions);
        File.WriteAllText(savePath, json);
        Debug.Log("����");
    }

    private void OnApplicationQuit()
    {
        SaveGodonPositions();
    }
}