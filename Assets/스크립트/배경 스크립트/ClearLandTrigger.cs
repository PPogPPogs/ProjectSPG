using UnityEngine;

public class ClearLandTrigger : MonoBehaviour
{
    public GameObject clearLandText; // 개간 텍스트 UI
    public GameObject terrainToClear; // 개간할 지형
    public GameObject clearedTerrainPrefab;
    private bool isInRange = false;

    private  void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            clearLandText.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
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
        Instantiate(clearedTerrainPrefab, terrainToClear.transform.position, Quaternion.identity);
        Destroy(terrainToClear);
    }
}

