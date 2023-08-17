using UnityEngine;

public class ClearLandTrigger : MonoBehaviour
{
    public GameObject clearLandText; // 개간 텍스트 UI
    public GameObject terrainToClear; // 개간할 지형

    private bool isInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            clearLandText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            clearLandText.SetActive(false);
        }
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.T))
        {
            ClearTerrain();
        }
    }

    private void ClearTerrain()
    {
        // 개간 로직 구현
        // terrainToClear를 수정하여 황무지를 개간된 지형으로 변경하는 코드를 작성하세요.
        // 예: terrainToClear.GetComponent<Terrain>().DoClear();
    }
}
