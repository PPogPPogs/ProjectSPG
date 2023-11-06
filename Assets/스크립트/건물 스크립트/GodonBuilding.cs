using UnityEngine;
using UnityEngine.UI;

public class GodonBuilding : MonoBehaviour
{
    public float constructionTime = 10.0f; // �ǹ� �Ǽ� �ð�(��)
    public float constructionTimePercent = 1.0f; // �ǹ� �Ǽ� �ð� ����(������)
    private float realcunstructionTime = 1.0f;
    private bool isUnderConstruction = false;
    private float constructionProgress = 0.0f;
    public Slider constructionBar;
    public GameObject Slider;
    private bool isConstruction = false;
    private bool isConstructioning = false;
    public Vector2 spawnPosition = new Vector2(-16.0f, -0.6f);// ����ƾ �� ��ǥ



    private void Start()
    {
        isConstruction = PlayerPrefs.GetInt("IsConstruction", 0) == 1;
        isConstructioning = PlayerPrefs.GetInt("IsConstructioning", 0) == 1;
        // �Ǽ� ���� ���¸� ��Ÿ�� UI �Ǵ� ���� ������Ʈ�� �����ɴϴ�.
        // ����: ConstructionBar�� �Ǽ� ���� ���¸� ǥ���ϴ� ���� ������Ʈ
        if (!isConstructioning)
        {
            Slider.SetActive(false); // �ʱ⿡�� ���ܵӴϴ�.
        }
        else 
        {
            StartConstruction();
            float savedConstructionProgress = PlayerPrefs.GetFloat("ConstructionProgress", 0.0f); // 0.0f�� �⺻��
            constructionProgress = savedConstructionProgress;

        }
        realcunstructionTime = constructionTime * constructionTimePercent;
        
        
        SavePosition();
    }

    private void Update()
    {
        if (isUnderConstruction)
        {
            // �Ǽ� ���� ��� Ÿ�̸Ӹ� ������Ʈ�մϴ�.
            constructionProgress += Time.deltaTime;

            // �Ǽ� ���� ���¸� ������Ʈ (UI �Ǵ� ���� ������Ʈ ǥ��)
            UpdateConstructionUI();

            // �Ǽ��� �Ϸ�Ǹ� �ǹ��� Ȱ��ȭ�ϰ� �Ǽ� ���� ����
            if (constructionProgress >= realcunstructionTime)
            {
                CompleteConstruction();
            }
        }
    }

    // �÷��̾ �ǹ� Ʈ���� ������ �� �� �Ǽ� ����
    

    // �Ǽ� ����
    public  void StartConstruction()
    {
        
        isUnderConstruction = true;
        Slider.SetActive(true);
        // �ٸ� �ʱ�ȭ �۾� ����
    }

    // �Ǽ� �Ϸ�
    private void CompleteConstruction()
    {
        PlayerPrefs.SetInt("IsConstruction", 0);
        PlayerPrefs.Save();
        isUnderConstruction = false;
        Slider.SetActive(false);
        // �ǹ��� Ȱ��ȭ�ϰų� �ٸ� �Ǽ� �Ϸ� �۾� ����
        gameObject.SetActive(true); // ����: �ǹ��� Ȱ��ȭ
        Vector3 spawnPosition3D = new Vector3(spawnPosition.x, spawnPosition.y, 0f);
        Justinmove justinmove = FindObjectOfType<Justinmove>();

        if (justinmove != null)
        {
            // SetTargetPosition �޼��带 ȣ���Ͽ� ��ǥ�� ����
            justinmove.SetHomePosition(spawnPosition);
            
        }

        PlayerPrefs.SetInt("IsConstructioning", 0);
        PlayerPrefs.Save();
        PlayerPrefs.DeleteKey("ConstructionProgress"); // "ConstructionProgress" Ű�� ����
        PlayerPrefs.Save(); // ���� ������ ����


    }

    private void UpdateConstructionUI()
    {
        // UI ������Ʈ �Ǵ� ���� ������Ʈ ������ �����Ͽ� �Ǽ� ���� ���¸� ǥ��
        // progress ���� 0.0 (����)���� 1.0 (�Ϸ�) ������ ���Դϴ�.

        // "constructionBar" ���� ������Ʈ�� �������� �����Ͽ� ü��� ȿ�� ����
        constructionBar.value = (float)constructionProgress / realcunstructionTime;
        PlayerPrefs.SetFloat("ConstructionProgress", constructionProgress);
        PlayerPrefs.Save();
    }

    private void SavePosition()
    {
        Vector3 position = transform.position;
        string key = "";

        if (position.x >= 2f && position.x <= 3f)
        {
            key = "2.5";
        }

        else if (position.x >= 6f && position.x <= 7f)
        {
            key = "6.5";
        }

        else if (position.x >= 10f && position.x <= 11f)
        {
            key = "10.5";
        }

        else if (position.x >= 14f && position.x <= 15f)
        {
            key = "14.5";
        }

        else if (position.x >= 18f && position.x <= 19f)
        {
            key = "18.5";
        }

        else if (position.x >= 22f && position.x <= 23f)
        {
            key = "22.5";
        }

        else if (position.x >= 26f && position.x <= 27f)
        {
            key = "26.5";
        }

        else if (position.x >= 30f && position.x <= 31f)
        {
            key = "30.5";
        }

        else if (position.x >= 34f && position.x <= 35f)
        {
            key = "34.5";
        }

        else if (position.x >= 38f && position.x <= 39f)
        {
            key = "38.5";
        }

        else if (position.x >= 42f && position.x <= 43f)
        {
            key = "42.5";
        }

        else if (position.x >= 46f && position.x <= 47f)
        {
            key = "46.5";
        }

        else if (position.x >= 50f && position.x <= 51f)
        {
            key = "50.5";
        }

        else if (position.x >= 54f && position.x <= 55f)
        {
            key = "54.5";
        }

        else if (position.x >= 58f && position.x <= 59f)
        {
            key = "58.5";
        }

        if (key != "")
        {
            PlayerPrefs.SetFloat($"{key}XGodonBuilding", position.x);
            PlayerPrefs.SetFloat($"{key}YGodonBuilding", position.y);
            PlayerPrefs.SetFloat($"{key}ZGodonBuilding", position.z);
            PlayerPrefs.Save();
        }
    }



}
