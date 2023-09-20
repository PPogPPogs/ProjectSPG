using UnityEngine;
using UnityEngine.UI;

public class buildingback : MonoBehaviour
{
    public GameObject objectToEnable;
    public GameObject objectToDisables;
    public Button activateButton; // UI ��ư�� ������ ����

    private void Start()
    {
        // ��ư�� Ŭ������ �� ȣ���� �Լ��� �����մϴ�.
        activateButton.onClick.AddListener(EnableObject);
    }

    // ������Ʈ Ȱ��ȭ �Լ�
    private void EnableObject()
    {
        objectToEnable.SetActive(false);
        objectToDisables.SetActive(false);

    }
}
