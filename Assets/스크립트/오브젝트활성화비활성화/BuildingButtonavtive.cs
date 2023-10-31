using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonactive : MonoBehaviour
{
    public GameObject objectToDisable1;
    public GameObject objectToDisable2;
    public GameObject objectToDisable3;
    public GameObject objectToDisable4;

    public Button deactivateButton; // UI 버튼을 연결할 변수

    private void Start()
    {
        // 버튼을 클릭했을 때 호출할 함수를 연결합니다.
        deactivateButton.onClick.AddListener(DisableObject);
    }

    // 오브젝트 비활성화 함수
    private void DisableObject()
    {
        objectToDisable1.SetActive(true);
        objectToDisable2.SetActive(false);
        objectToDisable3.SetActive(false);
        objectToDisable4.SetActive(false);

    }
}
