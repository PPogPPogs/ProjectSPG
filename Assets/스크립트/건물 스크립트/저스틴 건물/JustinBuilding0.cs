using UnityEngine;
using UnityEngine.UI;

public class JustinBuilding0 : MonoBehaviour
{
    public float constructionTime = 10.0f; // �ǹ� �Ǽ� �ð�(��)
    public float constructionTimePercent = 1.0f; // �ǹ� �Ǽ� �ð� ����(������)
    private float realcunstructionTime = 1.0f;
    private bool isUnderConstruction = false;
    private bool isInRange = false;
    private bool isTargetArrive = false;
    private float constructionProgress = 0.0f;
    
    public Image TimerImage;
    public GameObject UpgradeButton;
    private bool isConstruction = false;
    private bool isConstructioning = false;
    private bool JustinOneGo = false;
    private bool JustinRange = false;
    public Vector2 spawnPosition = new Vector2(-16.0f, -0.6f);// ����ƾ �� ��ǥ
    public GameObject JustinBuild0oj;
    public GameObject JustinBuild1oj;


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            isInRange = true;


        }
    }
   
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Justin"))
        {


            if (JustinRange)
            {
                return;
            }
            else
            {

                if (isTargetArrive)
                {
                    isConstructioning = PlayerPrefs.GetInt("IsConstructioning", 0) == 1;
                    if (isConstructioning)
                    {
                        StartConstruction();
                        JustinRange = true;

                    }
                }

            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            
        }
    }

    private void Upgrade()
    {
        UpgradeButton.SetActive(true);
    }

    private void Start()
    {
        
        isConstruction = PlayerPrefs.GetInt("IsConstruction", 0) == 1;
       
        // �Ǽ� ���� ���¸� ��Ÿ�� UI �Ǵ� ���� ������Ʈ�� �����ɴϴ�.
        // ����: ConstructionBar�� �Ǽ� ���� ���¸� ǥ���ϴ� ���� ������Ʈ
        if (!isConstructioning)
        {
            TimerImage.gameObject.SetActive(false); // �ʱ⿡�� ���ܵӴϴ�.
        }
        
        realcunstructionTime = constructionTime * constructionTimePercent;
    }

    private void Update()
    {
        isTargetArrive = PlayerPrefs.GetInt("IsTargetArrive", 0) == 1;
        
        if (isInRange && Input.GetKeyDown(KeyCode.Space))
        {
            Upgrade();
        }

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
    public void StartConstruction()
    {

        isUnderConstruction = true;
        TimerImage.gameObject.SetActive(true);
        float savedConstructionProgress = PlayerPrefs.GetFloat("ConstructionProgress", 0.0f); // 0.0f�� �⺻��
        constructionProgress = savedConstructionProgress;
        // �ٸ� �ʱ�ȭ �۾� ����
    }

    // �Ǽ� �Ϸ�
    private void CompleteConstruction()
    {
        PlayerPrefs.SetInt("IsConstruction", 0);
        PlayerPrefs.Save();
        isUnderConstruction = false;
        TimerImage.gameObject.SetActive(false);
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
        PlayerPrefs.SetInt("JustinBuild0Active", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("JustinBuild1Active", 1);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("IsTargetArrive", 0);
        PlayerPrefs.Save();
        JustinBuild1oj.SetActive(true);
        JustinBuild0oj.SetActive(false);

    }

    private void UpdateConstructionUI()
    {
        // UI ������Ʈ �Ǵ� ���� ������Ʈ ������ �����Ͽ� �Ǽ� ���� ���¸� ǥ��
        // progress ���� 0.0 (����)���� 1.0 (�Ϸ�) ������ ���Դϴ�.

        // "constructionBar" ���� ������Ʈ�� �������� �����Ͽ� ü��� ȿ�� ����
        TimerImage.fillAmount = (float)constructionProgress / realcunstructionTime;

        
        PlayerPrefs.SetFloat("ConstructionProgress", constructionProgress);
        PlayerPrefs.Save();
    }

}
