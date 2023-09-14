using UnityEngine;

public class Buildcastle : MonoBehaviour
{
    public GameObject clearLandText; // 개간 텍스트 UI
    public GameObject terrainToClear; // 개간할 지형
    public GameObject clearedTerrainPrefab;
    private bool isInRange = false;
   

    // 골드 추가량
    public int goldReward = 5; // 개간 시 얻는 골드 양
    public int WoodReward = 10;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
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
        // 골드 추가
        CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
        if (currencyManager != null)
        {
            currencyManager.AddCurrency(Currency.CurrencyType.Gold, goldReward);
            currencyManager.AddCurrency(Currency.CurrencyType.Wood, WoodReward);
        }

        // Prefab을 terrainToClear의 위치에서 오프셋만큼 이동시킨 후 생성
        Vector3 spawnPosition = terrainToClear.transform.position + new Vector3(0f, 0f, 0f); // 예: Y축으로 1만큼 이동
        Instantiate(clearedTerrainPrefab, spawnPosition, Quaternion.identity);
        Destroy(terrainToClear);

    }
}
