using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    public float constructionTime = 10.0f; // �ǹ� �Ǽ� �ð�(��)
    public float constructionTimePercent = 1.0f; // �ǹ� �Ǽ� �ð� ����(������)
    private float realcunstructionTime = 1.0f;
    private bool isUnderConstruction = false;
    private float constructionProgress = 0.0f;
    public Slider constructionBar;
    public GameObject Slider;
    private bool isConstruction = false;
    public Vector2 spawnPosition = new Vector2(-16.0f, -0.6f);// ����ƾ �� ��ǥ



    private void Start()
    {
        // �Ǽ� ���� ���¸� ��Ÿ�� UI �Ǵ� ���� ������Ʈ�� �����ɴϴ�.
        // ����: ConstructionBar�� �Ǽ� ���� ���¸� ǥ���ϴ� ���� ������Ʈ
        Slider.SetActive(false); // �ʱ⿡�� ���ܵӴϴ�.
        realcunstructionTime = constructionTime * constructionTimePercent;
        isConstruction = PlayerPrefs.GetInt("IsConstruction", 0) == 1;
        DontDestroyOnLoad(gameObject);
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
        
        
       

    }

    // �Ǽ� ���� ���� ������Ʈ (UI �Ǵ� ���� ������Ʈ ǥ��)
    // �Ǽ� ���� ���� ������Ʈ (UI �Ǵ� ���� ������Ʈ ǥ��)
    // �Ǽ� ���� ���� ������Ʈ (UI �Ǵ� ���� ������Ʈ ǥ��)
    private void UpdateConstructionUI()
    {
        // UI ������Ʈ �Ǵ� ���� ������Ʈ ������ �����Ͽ� �Ǽ� ���� ���¸� ǥ��
        // progress ���� 0.0 (����)���� 1.0 (�Ϸ�) ������ ���Դϴ�.

        // "constructionBar" ���� ������Ʈ�� �������� �����Ͽ� ü��� ȿ�� ����
        constructionBar.value = (float)constructionProgress / realcunstructionTime;
    }


}
