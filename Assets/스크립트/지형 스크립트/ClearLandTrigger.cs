using UnityEngine;

public class ClearLandTrigger : MonoBehaviour
{
    public GameObject clearLandText; // ���� �ؽ�Ʈ UI
    public GameObject terrainToClear; // ������ ����

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
        // ���� ���� ����
        // terrainToClear�� �����Ͽ� Ȳ������ ������ �������� �����ϴ� �ڵ带 �ۼ��ϼ���.
        // ��: terrainToClear.GetComponent<Terrain>().DoClear();
    }
}
