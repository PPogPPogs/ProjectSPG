using UnityEngine;
using UnityEngine.UI;

public class BuildBuilding2D : MonoBehaviour
{
    public GameObject buildingPrefab; // 건물 프리팹을 연결할 변수
    private float YourFixedYPosition = -0.65f;
    private bool isBuildingMode = false; // 건물을 짓는 모드인지 여부
    private GameObject previewBuilding = null;

    void Update()
    {
        // 건물 짓기 모드인 경우
        if (isBuildingMode)
        {
            // 마우스의 X 좌표를 얻어오고 Y 좌표는 고정값으로 설정
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // 2D 게임에서는 z 축을 0으로 설정
            mousePosition.y = (float)YourFixedYPosition; // 원하는 Y 좌표로 설정

            // 미리보기용 건물 오브젝트의 X 좌표만 업데이트
            previewBuilding.transform.position = new Vector3(mousePosition.x, (float)YourFixedYPosition, 0);

            if (Input.GetMouseButtonDown(0))
            {
                // 클릭한 위치에 건물을 배치
                Instantiate(buildingPrefab, mousePosition, Quaternion.identity);
                isBuildingMode = !isBuildingMode;
            }
        }
    }

    public void ToggleBuildingMode()
    {
        isBuildingMode = !isBuildingMode; // 건물 짓기 모드를 토글

        if (isBuildingMode)
        {
            // 마우스의 X 좌표를 얻어오고 Y 좌표는 고정값으로 설정
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // 2D 게임에서는 z 축을 0으로 설정
            mousePosition.y = (float)YourFixedYPosition; // 원하는 Y 좌표로 설정

            // 미리보기용 건물 오브젝트 생성
            previewBuilding = Instantiate(buildingPrefab, new Vector3(mousePosition.x, (float)YourFixedYPosition, 0), Quaternion.identity);
            // 미리보기용 건물 오브젝트를 처음에는 비활성화
            previewBuilding.SetActive(true);
        }
        else
        {
            // 건물 짓기 모드가 비활성화되면 미리보기용 건물 오브젝트 제거
            Destroy(previewBuilding);
        }
    }
}
