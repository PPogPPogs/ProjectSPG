using UnityEngine;
using UnityEngine.UI;

public class Buildingbutton : MonoBehaviour
{
    public GameObject objectToEnable;
    public GameObject objectToDisables;
    public Button activateButton; // UI ��ư�� ������ ����
    private Buildwall buildwall;
    public GameObject myObject;

    // �ߺ��� Start �޼��带 �ϳ��� ����
    private void Start()
    {
        // ��ư�� Ŭ������ �� ȣ���� �Լ��� �����մϴ�.
        activateButton.onClick.AddListener(EnableObject);
      
    }

    public void Update()
    {
        buildwall = FindObjectOfType<Buildwall>();
    }

    // ������Ʈ Ȱ��ȭ �Լ�
    private void EnableObject()
    {
        objectToEnable.SetActive(true);

        if (myObject.activeSelf)
        {
            Debug.Log("myObject�� Ȱ��ȭ��");
            if (buildwall != null)
            {
                buildwall.Comeback();
                Debug.Log("dd");
            }
            else
            {
                Debug.Log("buildwall�� null");
            }
        }
        else
        {
            Debug.Log("myObject�� ��Ȱ��ȭ��");
            objectToDisables.SetActive(true);
        }
    }
}
