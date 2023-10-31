using UnityEngine;

public class Buildcastle : MonoBehaviour
{
    public GameObject clearLandText; // 개간 텍스트 UI
    public GameObject terrainToClear; // 개간할 지형
    public GameObject clearedTerrainPrefab;
   
    private bool isInRange = false;

    // PlayerPrefs에 사용할 키
    private string Castle = "TerrainCleared";

    // 골드 추가량
    public int goldReward = 5; // 개간 시 얻는 골드 양
    public int WoodReward = 10;

    private void Start()
    {
        
        // PlayerPrefs에서 상태를 불러와서 건물을 설정
        if (PlayerPrefs.HasKey(Castle) && PlayerPrefs.GetInt(Castle) == 1)
        {
            SetClearedBuilding();
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
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
        if (isInRange && Input.GetKeyDown(KeyCode.T) && !PlayerPrefs.HasKey(Castle))
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

        // 개간 후 건물로 전환
        SetClearedBuilding();

        // PlayerPrefs에 상태 저장
        PlayerPrefs.SetInt(Castle, 1);
        PlayerPrefs.Save();
    }

    private void SetClearedBuilding()
    {
        // 개간 후 건물로 전환
        Destroy(terrainToClear);
        Instantiate(clearedTerrainPrefab, terrainToClear.transform.position, Quaternion.identity);
    }

   
}
