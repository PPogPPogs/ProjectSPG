using UnityEngine;

public class GodonBuilding1 : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Vector2 spawnPosition = new Vector2(6f, -0.6f); // 2D ��ǥ ����(�ʿ�)
    public GameObject objectToDisable;
    public GameObject BuildTextToDisable;
    public GameObject objectToOnable; // ��Ȱ��ȭ�� ������Ʈ�� ������ ����
    public GameObject ConstructionText;
    private bool isConstruction = false;

    public void OnButtonClick()
    {
        bool isConstruction = PlayerPrefs.GetInt("IsConstruction", 0) != 0;

        if (!isConstruction)
        {

            objectToOnable.SetActive(true);
            // ��ư�� ������ �� ȣ��Ǵ� �޼���
            // ������ 2D ��ǥ���� ������Ʈ�� �����մϴ�.
            Vector3 spawnPosition3D = new Vector3(spawnPosition.x, spawnPosition.y, 0f);
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition3D, Quaternion.identity);
            BuildTextToDisable.SetActive(false);


            // Justinmove ��ũ��Ʈ�� ã��
            Justinmove justinmove = FindObjectOfType<Justinmove>();

            if (justinmove != null)
            {
                // SetTargetPosition �޼��带 ȣ���Ͽ� ��ǥ�� ����
                justinmove.SetTargetPosition(spawnPosition);
                objectToDisable.SetActive(false);


            }
        }
        else
        {
            ConstructionText.SetActive(true);
        }
    }
}
