using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    
    public Vector2 spawnPosition = new Vector2(6f, -0.6f); // 2D 좌표 설정(필요)
    public GameObject objectToDisable;
    public GameObject ConstructionText;
    private bool isConstruction = false;
  

    public void OnButtonClick()
    {
         isConstruction = PlayerPrefs.GetInt("IsConstruction", 0) == 1;

        if (!isConstruction)
        {
            GameObject.Find("Justin").transform.Find("justin").gameObject.SetActive(true);
            // Justinmove 스크립트를 찾음
            Justinmove justinmove = FindObjectOfType<Justinmove>();

            if (justinmove != null)
            {
                // SetTargetPosition 메서드를 호출하여 좌표를 전달
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
