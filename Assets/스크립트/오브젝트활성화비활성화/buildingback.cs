using UnityEngine;
using UnityEngine.UI;

public class buildingback : MonoBehaviour
{
    public GameObject objectToEnable;
    public GameObject objectToDisables;
    public Button activateButton; // UI 버튼을 연결할 변수

    private void Start()
    {
        // 버튼을 클릭했을 때 호출할 함수를 연결합니다.
        activateButton.onClick.AddListener(EnableObject);
    }

    // 오브젝트 활성화 함수
    private void EnableObject()
    {
        objectToEnable.SetActive(false);
        objectToDisables.SetActive(false);

    }
}
