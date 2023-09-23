using UnityEngine;

public class buttonjustin : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Vector2 spawnPosition = new Vector2(6f, -0.6f); // 2D ��ǥ ����
    public GameObject objectToDisable; // ��Ȱ��ȭ�� ������Ʈ�� ������ ����

    public void OnButtonClick()
    {
        // ��ư�� ������ �� ȣ��Ǵ� �޼���
        // ������ 2D ��ǥ���� ������Ʈ�� �����մϴ�.
        Vector3 spawnPosition3D = new Vector3(spawnPosition.x, spawnPosition.y, 0f);
        Instantiate(prefabToSpawn, spawnPosition3D, Quaternion.identity);
        objectToDisable.SetActive(false);
    }
}
