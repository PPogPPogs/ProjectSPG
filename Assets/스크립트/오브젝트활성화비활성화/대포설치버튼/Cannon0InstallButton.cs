using UnityEngine;

public class Cannon0InstallButton : MonoBehaviour
{
    public GameObject Cannon0Prefab;
    public Vector3 Cannon0spawnPosition;
    public Vector2 spawnPosition = new Vector2(6f, -0.6f); // 2D ��ǥ ����(�ʿ�)
    public GameObject objectToDisable;
    public GameObject BuildTextToDisable;
    public GameObject objectToOnable; // ��Ȱ��ȭ�� ������Ʈ�� ������ ����
    public GameObject ConstructionText;
    private bool isConstruction = false;

    public void OnButtonClick()
    {
        bool isConstruction = PlayerPrefs.GetInt("IsConstruction", 0) != 0;

        if (!isConstruction)
        {

            objectToOnable.SetActive(true);
            // ��ư�� ������ �� ȣ��Ǵ� �޼���
            // ������ 2D ��ǥ���� ������Ʈ�� �����մϴ�.
           
            BuildTextToDisable.SetActive(false);
            GameObject Cannon0 = Instantiate(Cannon0Prefab, Cannon0spawnPosition, Quaternion.identity);



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
