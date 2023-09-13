using UnityEngine;
using UnityEngine.UI;

public class BuildBuilding2D : MonoBehaviour
{
    public GameObject buildingPrefab; // �ǹ� �������� ������ ����
    private float YourFixedYPosition = -0.65f;
    private bool isBuildingMode = false; // �ǹ��� ���� ������� ����
    private GameObject previewBuilding = null;

    void Update()
    {
        // �ǹ� ���� ����� ���
        if (isBuildingMode)
        {
            // ���콺�� X ��ǥ�� ������ Y ��ǥ�� ���������� ����
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // 2D ���ӿ����� z ���� 0���� ����
            mousePosition.y = (float)YourFixedYPosition; // ���ϴ� Y ��ǥ�� ����

            // �̸������ �ǹ� ������Ʈ�� X ��ǥ�� ������Ʈ
            previewBuilding.transform.position = new Vector3(mousePosition.x, (float)YourFixedYPosition, 0);

            if (Input.GetMouseButtonDown(0))
            {
                // Ŭ���� ��ġ�� �ǹ��� ��ġ
                Instantiate(buildingPrefab, mousePosition, Quaternion.identity);
                isBuildingMode = !isBuildingMode;
            }
        }
    }

    public void ToggleBuildingMode()
    {
        isBuildingMode = !isBuildingMode; // �ǹ� ���� ��带 ���

        if (isBuildingMode)
        {
            // ���콺�� X ��ǥ�� ������ Y ��ǥ�� ���������� ����
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // 2D ���ӿ����� z ���� 0���� ����
            mousePosition.y = (float)YourFixedYPosition; // ���ϴ� Y ��ǥ�� ����

            // �̸������ �ǹ� ������Ʈ ����
            previewBuilding = Instantiate(buildingPrefab, new Vector3(mousePosition.x, (float)YourFixedYPosition, 0), Quaternion.identity);
            // �̸������ �ǹ� ������Ʈ�� ó������ ��Ȱ��ȭ
            previewBuilding.SetActive(true);
        }
        else
        {
            // �ǹ� ���� ��尡 ��Ȱ��ȭ�Ǹ� �̸������ �ǹ� ������Ʈ ����
            Destroy(previewBuilding);
        }
    }
}
