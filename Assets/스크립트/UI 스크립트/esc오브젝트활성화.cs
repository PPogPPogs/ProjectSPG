using UnityEngine;

public class DisableObjectOnEsc : MonoBehaviour
{
    public GameObject objectToDisable;

    private bool isDisabled = true; // true�� �ʱ�ȭ

    private void Start()
    {
        // ���� ���� �ÿ� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        objectToDisable.SetActive(false);
    }

    private void Update()
    {
        // Esc Ű�� ������ ������Ʈ Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isDisabled = !isDisabled;

            if (isDisabled)
            {
                // ������Ʈ�� ��Ȱ��ȭ�մϴ�.
                objectToDisable.SetActive(false);
            }
            else
            {
                // ������Ʈ�� Ȱ��ȭ�մϴ�.
                objectToDisable.SetActive(true);
            }
        }
    }
}
