using UnityEngine;
using UnityEngine.UI;

public class Cannon11 : MonoBehaviour
{
    public float constructionTime = 10.0f; // 건물 건설 시간(초)
    public float constructionTimePercent = 1.0f; // 건물 건설 시간 배율(조종값)
    private float realcunstructionTime = 1.0f;
    private bool isUnderConstruction = false;
    private float constructionProgress = 0.0f;
    public Slider constructionBar;
    public GameObject Slider;
    private bool isConstruction = false;
    private bool isConstructioning = false;
    public Vector2 spawnPosition = new Vector2(-16.0f, -0.6f);// 저스틴 집 좌표
    public GameObject Cannon11oj;
    public GameObject Cannon12oj;




    private void Start()
    {
        isConstruction = PlayerPrefs.GetInt("IsConstruction", 0) == 1;
        isConstructioning = PlayerPrefs.GetInt("IsConstructioning", 0) == 1;
        // 건설 진행 상태를 나타낼 UI 또는 게임 오브젝트를 가져옵니다.
        // 예시: ConstructionBar는 건설 진행 상태를 표시하는 게임 오브젝트
        if (!isConstructioning)
        {
            Slider.SetActive(false); // 초기에는 숨겨둡니다.
        }
        else
        {
            StartConstruction();
            float savedConstructionProgress = PlayerPrefs.GetFloat("ConstructionProgress", 0.0f); // 0.0f는 기본값
            constructionProgress = savedConstructionProgress;

        }
        realcunstructionTime = constructionTime * constructionTimePercent;



    }

    private void Update()
    {
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
        Slider.SetActive(true);
        // 다른 초기화 작업 수행
    }

    // 건설 완료
    private void CompleteConstruction()
    {
        PlayerPrefs.SetInt("IsConstruction", 0);
        PlayerPrefs.Save();
        isUnderConstruction = false;
        Slider.SetActive(false);
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
        PlayerPrefs.SetInt("Cannon11Active", 0);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("Cannon12Active", 1);
        PlayerPrefs.Save();
        Cannon12oj.SetActive(true);
        Cannon11oj.SetActive(false);
    }

    private void UpdateConstructionUI()
    {
        // UI 업데이트 또는 게임 오브젝트 조작을 수행하여 건설 진행 상태를 표시
        // progress 값은 0.0 (시작)에서 1.0 (완료) 사이의 값입니다.

        // "constructionBar" 게임 오브젝트의 스케일을 조절하여 체우기 효과 생성
        constructionBar.value = (float)constructionProgress / realcunstructionTime;
        PlayerPrefs.SetFloat("ConstructionProgress", constructionProgress);
        PlayerPrefs.Save();
    }




}
