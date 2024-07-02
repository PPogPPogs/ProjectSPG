using UnityEngine;
using UnityEngine.UI;

public class JustinBuilding0 : MonoBehaviour
{
    public float constructionTime = 10.0f; // 건물 건설 시간(초)
    public float constructionTimePercent = 1.0f; // 건물 건설 시간 배율(조종값)
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
    public Vector2 spawnPosition = new Vector2(-16.0f, -0.6f);// 저스틴 집 좌표
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
       
        // 건설 진행 상태를 나타낼 UI 또는 게임 오브젝트를 가져옵니다.
        // 예시: ConstructionBar는 건설 진행 상태를 표시하는 게임 오브젝트
        if (!isConstructioning)
        {
            TimerImage.gameObject.SetActive(false); // 초기에는 숨겨둡니다.
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
            // 건설 중인 경우 타이머를 업데이트합니다.
            constructionProgress += Time.deltaTime;

            // 건설 진행 상태를 업데이트 (UI 또는 게임 오브젝트 표시)
            UpdateConstructionUI();

            // 건설이 완료되면 건물을 활성화하고 건설 상태 종료
            if (constructionProgress >= realcunstructionTime)
            {
                CompleteConstruction();
            }
        }
    }

    // 플레이어가 건물 트리거 영역에 들어갈 때 건설 시작


    // 건설 시작
    public void StartConstruction()
    {

        isUnderConstruction = true;
        TimerImage.gameObject.SetActive(true);
        float savedConstructionProgress = PlayerPrefs.GetFloat("ConstructionProgress", 0.0f); // 0.0f는 기본값
        constructionProgress = savedConstructionProgress;
        // 다른 초기화 작업 수행
    }

    // 건설 완료
    private void CompleteConstruction()
    {
        PlayerPrefs.SetInt("IsConstruction", 0);
        PlayerPrefs.Save();
        isUnderConstruction = false;
        TimerImage.gameObject.SetActive(false);
        // 건물을 활성화하거나 다른 건설 완료 작업 수행
        gameObject.SetActive(true); // 예시: 건물을 활성화
        Vector3 spawnPosition3D = new Vector3(spawnPosition.x, spawnPosition.y, 0f);
        Justinmove justinmove = FindObjectOfType<Justinmove>();

        if (justinmove != null)
        {
            // SetTargetPosition 메서드를 호출하여 좌표를 전달
            justinmove.SetHomePosition(spawnPosition);

        }

        PlayerPrefs.SetInt("IsConstructioning", 0);
        PlayerPrefs.Save();
        PlayerPrefs.DeleteKey("ConstructionProgress"); // "ConstructionProgress" 키를 삭제
        PlayerPrefs.Save(); // 변경 사항을 저장
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
        // UI 업데이트 또는 게임 오브젝트 조작을 수행하여 건설 진행 상태를 표시
        // progress 값은 0.0 (시작)에서 1.0 (완료) 사이의 값입니다.

        // "constructionBar" 게임 오브젝트의 스케일을 조절하여 체우기 효과 생성
        TimerImage.fillAmount = (float)constructionProgress / realcunstructionTime;

        
        PlayerPrefs.SetFloat("ConstructionProgress", constructionProgress);
        PlayerPrefs.Save();
    }

}
