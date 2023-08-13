using UnityEngine;

public class DisableObjectOnEsc : MonoBehaviour
{
    public GameObject objectToDisable;

    private bool isDisabled = true; // true로 초기화

    private void Start()
    {
        // 게임 시작 시에 오브젝트를 비활성화합니다.
        objectToDisable.SetActive(false);
    }

    private void Update()
    {
        // Esc 키를 누르면 오브젝트 활성화/비활성화
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isDisabled = !isDisabled;

            if (isDisabled)
            {
                // 오브젝트를 비활성화합니다.
                objectToDisable.SetActive(false);
            }
            else
            {
                // 오브젝트를 활성화합니다.
                objectToDisable.SetActive(true);
            }
        }
    }
}
