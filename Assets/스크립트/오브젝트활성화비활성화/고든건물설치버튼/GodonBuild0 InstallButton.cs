using UnityEngine;

public class GodonBuild0InstallButton : MonoBehaviour
{
    public GameObject GodonBuild0Prefab;
    public Vector3 GodonBuild0spawnPosition;
    public Vector2 spawnPosition = new Vector2(6f, -0.6f); // 2D 좌표 설정(필요)
    public GameObject objectToDisable;
    public GameObject BuildTextToDisable;
    public GameObject objectToOnable; // 비활성화할 오브젝트를 연결할 변수
    public GameObject ConstructionText;
    private bool isConstruction = false;

    public void OnButtonClick()
    {
        bool isConstruction = PlayerPrefs.GetInt("IsConstruction", 0) != 0;

        if (!isConstruction)
        {

            objectToOnable.SetActive(true);
            // 버튼을 눌렀을 때 호출되는 메서드
            // 지정된 2D 좌표에서 오브젝트를 생성합니다.

            BuildTextToDisable.SetActive(false);
            GameObject GodonBuild0 = Instantiate(GodonBuild0Prefab, GodonBuild0spawnPosition, Quaternion.identity);



            // Justinmove 스크립트를 찾음
            Justinmove justinmove = FindObjectOfType<Justinmove>();

            if (justinmove != null)
            {
                // SetTargetPosition 메서드를 호출하여 좌표를 전달
                justinmove.SetTargetPosition(spawnPosition);
                objectToDisable.SetActive(false);


            }
        }
        else
        {
            ConstructionText.SetActive(true);
        }
    }
}
