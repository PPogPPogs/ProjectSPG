using UnityEngine;

public class buttonjustin : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Vector2 spawnPosition = new Vector2(6f, -0.6f); // 2D 좌표 설정
    public GameObject objectToDisable; // 비활성화할 오브젝트를 연결할 변수

    public void OnButtonClick()
    {
        // 버튼을 눌렀을 때 호출되는 메서드
        // 지정된 2D 좌표에서 오브젝트를 생성합니다.
        Vector3 spawnPosition3D = new Vector3(spawnPosition.x, spawnPosition.y, 0f);
        Instantiate(prefabToSpawn, spawnPosition3D, Quaternion.identity);
        objectToDisable.SetActive(false);
    }
}
