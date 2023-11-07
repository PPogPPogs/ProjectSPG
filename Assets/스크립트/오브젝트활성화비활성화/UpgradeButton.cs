using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    
    public Vector2 spawnPosition = new Vector2(6f, -0.6f); // 2D ��ǥ ����(�ʿ�)
    public GameObject objectToDisable;
    public GameObject ConstructionText;
    private bool isConstruction = false;

    public void OnButtonClick()
    {
        bool isConstruction = PlayerPrefs.GetInt("IsConstruction", 0) != 0;

        if (!isConstruction)
        {
                   
            // Justinmove ��ũ��Ʈ�� ã��
            Justinmove justinmove = FindObjectOfType<Justinmove>();

            if (justinmove != null)
            {
                // SetTargetPosition �޼��带 ȣ���Ͽ� ��ǥ�� ����
                justinmove.SetTargetPosition(spawnPosition);
                objectToDisable.SetActive(false);


            }
        }
        else
        {
            ConstructionText.SetActive(true);
        }
    }
}
