using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject BuildingText; // 개간 텍스트 UI
    public GameObject BuildingImage; // 건물들 이미지
    private bool isInRange = false;
    private bool isDefenseTower = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DefenseTower"))
        {
            isInRange = false;
            isDefenseTower = true;
            Debug.Log("ddd");
        }
        if (other.CompareTag("Player") && !isDefenseTower)
        {
            isInRange = true;
            BuildingText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            BuildingText.SetActive(false);
        }
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.Space) )
        {
            ClearTerrain();
        }
    }

    private void ClearTerrain()
    {
        BuildingImage.SetActive(true);
    }

    

}
