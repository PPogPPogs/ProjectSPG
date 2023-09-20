using UnityEngine;
using UnityEngine.UI;

public class Buildingbutton : MonoBehaviour
{
    public GameObject objectToEnable;
    public GameObject objectToDisables;
    public Button activateButton; // UI 버튼을 연결할 변수
    private Buildwall buildwall;
    public GameObject myObject;

    // 중복된 Start 메서드를 하나로 통합
    private void Start()
    {
        // 버튼을 클릭했을 때 호출할 함수를 연결합니다.
        activateButton.onClick.AddListener(EnableObject);
      
    }

    public void Update()
    {
        buildwall = FindObjectOfType<Buildwall>();
    }

    // 오브젝트 활성화 함수
    private void EnableObject()
    {
        objectToEnable.SetActive(true);

        if (myObject.activeSelf)
        {
            Debug.Log("myObject는 활성화됨");
            if (buildwall != null)
            {
                buildwall.Comeback();
                Debug.Log("dd");
            }
            else
            {
                Debug.Log("buildwall은 null");
            }
        }
        else
        {
            Debug.Log("myObject는 비활성화됨");
            objectToDisables.SetActive(true);
        }
    }
}
