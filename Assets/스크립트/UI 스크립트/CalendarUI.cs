using UnityEngine;
using UnityEngine.UI;

public class CalendarUI : MonoBehaviour
{
    
    public Text monthText;   // 월 텍스트 UI 요소
    public Text dayText;     // 일 텍스트 UI 요소
    public Text hourText;    // 시간 텍스트 UI 요소
    public Text minuteText;  // 분 텍스트 UI 요소
    public CalendarManager calendarManager;

    public void UpdateUI()
    {
        // 현재 게임 내 시간과 날짜를 가져와서 표시
       
        int currentMonth = calendarManager.GetMonth();
        int currentDay = calendarManager.GetDay();
        int currentHour = calendarManager.GetHour();
        int currentMinute = calendarManager.GetMinute();

        // 년, 월, 일, 시간, 분을 텍스트로 표시
        
        monthText.text = currentMonth + "월";
        dayText.text = currentDay + "일";
        hourText.text = currentHour + "시";
        minuteText.text = currentMinute + "분";
    }

    private void Start()
    {
        UpdateUI();
    }
}
