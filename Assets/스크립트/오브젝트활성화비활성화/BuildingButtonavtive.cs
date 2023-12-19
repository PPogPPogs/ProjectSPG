using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonactive : MonoBehaviour
{
    public GameObject objectable;
    public GameObject objectToDisable;
   
    public Button deactivateButton; // UI 버튼을 연결할 변수

    private void Start()
    {
        // 버튼을 클릭했을 때 호출할 함수를 연결합니다.
        deactivateButton.onClick.AddListener(DisableObject);
    }

    // 오브젝트 비활성화 함수
    private void DisableObject()
    {
        objectable.SetActive(true);
        objectToDisable.SetActive(false);
      
    }
}
