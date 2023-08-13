using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // �̵��� ���� �ε��� ��ȣ
    public int nextSceneIndex;

    // ĳ���� ���� ������Ʈ
    public GameObject character;

    void Start()
    {
        // ĳ���Ͱ� �� ��ȯ �� �������� �ʵ��� ����
        DontDestroyOnLoad(character);
    }

    // ��ư Ŭ�� �� ȣ��� �Լ�
    public void ChangeScene()
    {
        // �� ��ȯ
        SceneManager.LoadScene(nextSceneIndex);
    }
}
