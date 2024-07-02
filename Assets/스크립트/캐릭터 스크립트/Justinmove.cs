using UnityEngine;

public class Justinmove : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    private bool isMoving = false;
    private bool isHomeComing = false;
    private bool isArrive = false;
    private bool isJustinActive = true;
    private Animator animator;
    public GameObject Justin;
    private Vector2 targetPosition; // 이동할 좌표
    private  Vector3 newScale = new Vector3(3.0f, 3.0f, 1.0f);
    private string JustinX = "justinX";
    private string JustinY = "justinY";
    private string JustinZ = "justinZ";
    private bool isConstruction = false;
    private bool isConstructioning = false;
    private bool isTargetArrive = false;


    private void Start()
    {
        
        animator = GetComponent<Animator>();
        Vector3 position = JustinPosition();
        transform.position = position;
        isConstruction = PlayerPrefs.GetInt("IsConstruction", 0) == 1;
        isConstructioning = PlayerPrefs.GetInt("IsConstructioning", 0) == 1;
        int justinActive = PlayerPrefs.GetInt("JustinActive", 1); // 기본값은 활성화 (1)
        Justin.SetActive(justinActive == 1);
        float savedTargetX = PlayerPrefs.GetFloat("JustinSavedTargetX", float.NaN); // NaN을 기본값으로 설정
        float savedTargetY = PlayerPrefs.GetFloat("JustinSavedTargetY", float.NaN); // NaN을 기본값으로 설정

        // 저장된 좌표가 NaN이 아니면 SetTargetPosition 함수를 호출하여 저장된 좌표로 이동
        if (!float.IsNaN(savedTargetX) && !float.IsNaN(savedTargetY))
        {
            SetTargetPosition(new Vector2(savedTargetX, savedTargetY));
        }

        float savedHomeX = PlayerPrefs.GetFloat("JustinSavedHomeX", float.NaN); // NaN을 기본값으로 설정
        float savedHomeY = PlayerPrefs.GetFloat("JustinSavedHomeY", float.NaN); // NaN을 기본값으로 설정

        // 저장된 좌표가 NaN이 아니면 SetTargetPosition 함수를 호출하여 저장된 좌표로 이동
        if (!float.IsNaN(savedHomeX) && !float.IsNaN(savedHomeY))
        {
            SetHomePosition(new Vector2(savedHomeX, savedHomeY));
        }
    }

    public void SetTargetPosition(Vector2 position)
    {
        targetPosition = position;
        PlayerPrefs.SetFloat("JustinSavedTargetX", targetPosition.x); // x 좌표 저장
        PlayerPrefs.SetFloat("JustinSavedTargetY", targetPosition.y); // y 좌표 저장
        PlayerPrefs.Save();
        isMoving = true;
        isHomeComing = false;
    }


    public void SetHomePosition(Vector2 position)
    {
        targetPosition = position;
        PlayerPrefs.SetFloat("JustinSavedHomeX", targetPosition.x); // x 좌표 저장
        PlayerPrefs.SetFloat("JustinSavedHomeY", targetPosition.y); // y 좌표 저장
        PlayerPrefs.Save();
        isHomeComing = true;
        isMoving = false;

    }


    private void Update()
    {
        SaveJustinPosition(transform.position);

        if (isMoving)
        {
            MoveToTarget();
            
        }
        if (isHomeComing)
        {
            MoveToHome();
           
        }
        if (isConstructioning)
        {
            animator.SetTrigger("Building");
        }
    }



    public void MoveToTarget()
    {
        PlayerPrefs.SetInt("IsConstruction", 1);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("JustinActive", 1);
        PlayerPrefs.Save();
        isArrive = false;
        float distanceToTargetScale = targetPosition.x - transform.position.x;
        if (distanceToTargetScale > 0.1f)
        {
            transform.localScale = new Vector3(newScale.x, newScale.y, newScale.z); // newScale을 Vector3로 변환하여 할당
        }
        else if(distanceToTargetScale < -0.1f)
        {
            transform.localScale = new Vector3(-newScale.x, newScale.y, newScale.z);
        }

        // 이동 로직을 적용합니다.
        Vector3 direction = new Vector3(targetPosition.x, targetPosition.y, transform.position.z) - transform.position;
        direction.Normalize();

        animator.SetBool("IsRunning", true);
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        float distanceToTarget = Mathf.Abs(targetPosition.x - transform.position.x); // x 좌표만 고려한 거리 계산

        if (distanceToTarget < 0.1f) // 거리 임계값을 조정
        {
            if (!isArrive)
            {
                TargetArrive();
            }
        }
    }

    public void MoveToHome()
    {
        isArrive = false;
        transform.localScale = new Vector3(-newScale.x, newScale.y, newScale.z); // newScale을 Vector3로 변환하여 할당

        // 이동 로직을 적용합니다.
        Vector3 direction = new Vector3(targetPosition.x, targetPosition.y, transform.position.z) - transform.position;
        direction.Normalize();

        animator.SetBool("IsRunning", true);
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        float distanceToTarget = Mathf.Abs(targetPosition.x - transform.position.x); // x 좌표만 고려한 거리 계산

        if (distanceToTarget < 0.1f) // 거리 임계값을 조정
        {
            if (!isArrive)
            {
                HomeArrive();
            }
        }
    }

    public void TargetArrive()
    {
        PlayerPrefs.SetInt("IsConstructioning", 1);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("IsTargetArrive", 1);
        PlayerPrefs.Save();
        PlayerPrefs.DeleteKey("JustinSavedTargetX");
        PlayerPrefs.DeleteKey("JustinSavedTargetY");
        PlayerPrefs.Save();
        isArrive = true;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Building");
        isMoving = false; // 이동 중이 아니라고 표시
        isHomeComing = false; // 이동 중이 아니라고 표시
        
     
    }
    
    private Vector3 JustinPosition()
    {
        float x = PlayerPrefs.GetFloat(JustinX, -22.5f); // 0f는 기본 위치
        float y = PlayerPrefs.GetFloat(JustinY, -0.63f);
        float z = PlayerPrefs.GetFloat(JustinZ, 0f);
        return new Vector3(x, y, z);

    }

    public void HomeArrive()
    {
        PlayerPrefs.DeleteKey("JustinSavedHomeX");
        PlayerPrefs.DeleteKey("JustinSavedHomeY");
        PlayerPrefs.Save();
        Justin.SetActive(false);
        isArrive = true;
        animator.SetBool("IsRunning", false);
        isMoving = false;
        isHomeComing = false;
        PlayerPrefs.SetInt("JustinActive", 0);
        PlayerPrefs.Save();
    }


    public void SaveJustinPosition(Vector3 position)
    {


        PlayerPrefs.SetFloat(JustinX, position.x);
        PlayerPrefs.SetFloat(JustinY, position.y);
        PlayerPrefs.SetFloat(JustinZ, position.z);
        PlayerPrefs.Save();

    }
}
