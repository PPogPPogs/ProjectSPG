using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonactive : MonoBehaviour
{
    public GameObject objectable;
    public GameObject objectToDisable;
   
    public Button deactivateButton; // UI ��ư�� ������ ����

    private void Start()
    {
        // ��ư�� Ŭ������ �� ȣ���� �Լ��� �����մϴ�.
        deactivateButton.onClick.AddListener(DisableObject);
    }

    // ������Ʈ ��Ȱ��ȭ �Լ�
    private void DisableObject()
    {
        objectable.SetActive(true);
        objectToDisable.SetActive(false);
      
    }
}
