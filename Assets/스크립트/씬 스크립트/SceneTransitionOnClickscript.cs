using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionOnClick : MonoBehaviour
{
    public string nextSceneName; // 다음 씬의 이름

    void Update()
    {
        // 화면을 클릭했을 때 다음 씬으로 전환
        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 클릭
        {
            // 다음 씬으로 전환하는 메서드 호출
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // SceneManager를 사용하여 다음 씬으로 전환
        SceneManager.LoadScene(nextSceneName);
    }
}
