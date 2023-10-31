using UnityEngine;

public class Buildcastle : MonoBehaviour
{
    public GameObject clearLandText; // ���� �ؽ�Ʈ UI
    public GameObject terrainToClear; // ������ ����
    public GameObject clearedTerrainPrefab;
   
    private bool isInRange = false;

    // PlayerPrefs�� ����� Ű
    private string Castle = "TerrainCleared";

    // ��� �߰���
    public int goldReward = 5; // ���� �� ��� ��� ��
    public int WoodReward = 10;

    private void Start()
    {
        
        // PlayerPrefs���� ���¸� �ҷ��ͼ� �ǹ��� ����
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
        // ��� �߰�
        CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
        if (currencyManager != null)
        {
            currencyManager.AddCurrency(Currency.CurrencyType.Gold, goldReward);
            currencyManager.AddCurrency(Currency.CurrencyType.Wood, WoodReward);
        }

        // ���� �� �ǹ��� ��ȯ
        SetClearedBuilding();

        // PlayerPrefs�� ���� ����
        PlayerPrefs.SetInt(Castle, 1);
        PlayerPrefs.Save();
    }

    private void SetClearedBuilding()
    {
        // ���� �� �ǹ��� ��ȯ
        Destroy(terrainToClear);
        Instantiate(clearedTerrainPrefab, terrainToClear.transform.position, Quaternion.identity);
    }

   
}
