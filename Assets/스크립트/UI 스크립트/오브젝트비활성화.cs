using UnityEngine;
using UnityEngine.UI;

public class ������Ʈ��Ȱ��ȭ : MonoBehaviour
{
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
        objectToDisable.SetActive(false);
    }
}
