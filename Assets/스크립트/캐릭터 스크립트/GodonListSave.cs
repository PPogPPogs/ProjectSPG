using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class GodonListSave : MonoBehaviour
{
    [SerializeField] private GameObject godonPrefab;

    private string savePath = "path/to/save/file.json";

    private List<SerializableVector3> godonPositions = new List<SerializableVector3>();

    private void Awake()
    {
        LoadAllMonsterPositions();
    }

    private void LoadAllMonsterPositions()
    {
        List<SerializableVector3> savedPositions = GetSavedGodonPositions();

        foreach (SerializableVector3 position in savedPositions)
        {
            InstantiateGodon(position.ToVector3());
            Debug.Log("��ġ����");
        }
    }

    private List<SerializableVector3> GetSavedGodonPositions()
    {
        List<SerializableVector3> savedPositions = new List<SerializableVector3>();

        string json = File.ReadAllText(savePath);
        savedPositions = JsonConvert.DeserializeObject<List<SerializableVector3>>(json);

        return savedPositions;
    }

    private void InstantiateGodon(Vector3 position)
    {
        GameObject godonObject = Instantiate(godonPrefab, position, Quaternion.identity);
        // Godon ��ũ��Ʈ�� ���� �߰� �۾��� �ʿ��� �� �ֽ��ϴ�.
    }

    public void SaveGodonPositions()
    {
        godonPositions.Clear();

        // ���� Godon ��ġ ������ ����Ʈ�� ����
        // godonList ��� ����ȭ�� ����Ʈ�� ����մϴ�.
        foreach (GameObject godonObject in GameObject.FindGameObjectsWithTag("Godon"))
        {
            Godon godon = godonObject.GetComponent<Godon>();
            Vector3 position = godon.LoadGodonPosition();
            godonPositions.Add(new SerializableVector3(position));
            Debug.Log("����?");
        }

        // ����Ʈ�� JSON���� ��ȯ�Ͽ� ���Ͽ� ����
        string json = JsonConvert.SerializeObject(godonPositions);
        File.WriteAllText(savePath, json);
        Debug.Log("����");
    }

    private void OnApplicationQuit()
    {
        SaveGodonPositions();
    }
}