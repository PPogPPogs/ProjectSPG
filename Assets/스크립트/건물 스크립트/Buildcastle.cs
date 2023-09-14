using UnityEngine;

public class Buildcastle : MonoBehaviour
{
    public GameObject clearLandText; // ���� �ؽ�Ʈ UI
    public GameObject terrainToClear; // ������ ����
    public GameObject clearedTerrainPrefab;
    private bool isInRange = false;
   

    // ��� �߰���
    public int goldReward = 5; // ���� �� ��� ��� ��
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
        // ��� �߰�
        CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
        if (currencyManager != null)
        {
            currencyManager.AddCurrency(Currency.CurrencyType.Gold, goldReward);
            currencyManager.AddCurrency(Currency.CurrencyType.Wood, WoodReward);
        }

        // Prefab�� terrainToClear�� ��ġ���� �����¸�ŭ �̵���Ų �� ����
        Vector3 spawnPosition = terrainToClear.transform.position + new Vector3(0f, 0f, 0f); // ��: Y������ 1��ŭ �̵�
        Instantiate(clearedTerrainPrefab, spawnPosition, Quaternion.identity);
        Destroy(terrainToClear);

    }
}
