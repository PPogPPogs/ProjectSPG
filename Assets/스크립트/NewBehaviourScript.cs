using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public void OnDeleteAllDataButton()
    {
        // PlayerPrefs�� ����� ��� ������ ����
        PlayerPrefs.DeleteAll();
        Debug.Log("�����Ϸ�");
    }
}
