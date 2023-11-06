using UnityEngine;
using UnityEngine.UI;

public class GodonBuilding : MonoBehaviour
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
        
        
        SavePosition();
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
    public  void StartConstruction()
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
