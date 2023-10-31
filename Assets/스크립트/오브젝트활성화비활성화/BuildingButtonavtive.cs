using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonactive : MonoBehaviour
{
    public GameObject objectToDisable1;
    public GameObject objectToDisable2;
    public GameObject objectToDisable3;
    public GameObject objectToDisable4;

    public Button deactivateButton; // UI ��ư�� ������ ����

    private void Start()
    {
        // ��ư�� Ŭ������ �� ȣ���� �Լ��� �����մϴ�.
        deactivateButton.onClick.AddListener(DisableObject);
    }

    // ������Ʈ ��Ȱ��ȭ �Լ�
    private void DisableObject()
    {
        objectToDisable1.SetActive(true);
        objectToDisable2.SetActive(false);
        objectToDisable3.SetActive(false);
        objectToDisable4.SetActive(false);

    }
}
