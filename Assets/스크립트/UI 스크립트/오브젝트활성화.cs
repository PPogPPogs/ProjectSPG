using UnityEngine;
using UnityEngine.UI;

public class ������ƮȰ��ȭ : MonoBehaviour
{
    public GameObject objectToEnable;
    public Button activateButton; // UI ��ư�� ������ ����

    private void Start()
    {
        // ��ư�� Ŭ������ �� ȣ���� �Լ��� �����մϴ�.
        activateButton.onClick.AddListener(EnableObject);
    }

    // ������Ʈ Ȱ��ȭ �Լ�
    private void EnableObject()
    {
        objectToEnable.SetActive(true);
    }
}
