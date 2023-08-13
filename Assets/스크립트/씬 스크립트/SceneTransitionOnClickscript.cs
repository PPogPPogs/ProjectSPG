using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionOnClick : MonoBehaviour
{
    public string nextSceneName; // ���� ���� �̸�

    void Update()
    {
        // ȭ���� Ŭ������ �� ���� ������ ��ȯ
        if (Input.GetMouseButtonDown(0)) // ���� ���콺 Ŭ��
        {
            // ���� ������ ��ȯ�ϴ� �޼��� ȣ��
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // SceneManager�� ����Ͽ� ���� ������ ��ȯ
        SceneManager.LoadScene(nextSceneName);
    }
}
