using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // 이동할 씬의 인덱스 번호
    public int nextSceneIndex;

    // 캐릭터 게임 오브젝트
    public GameObject character;

    void Start()
    {
        // 캐릭터가 씬 전환 시 삭제되지 않도록 설정
        DontDestroyOnLoad(character);
    }

    // 버튼 클릭 시 호출될 함수
    public void ChangeScene()
    {
        // 씬 전환
        SceneManager.LoadScene(nextSceneIndex);
    }
}
