using UnityEngine;

public class CalendarManager : MonoBehaviour
{
    public float gameSecondsPerRealSecond = 2.0f; // 게임 내 시간이 실제 시간보다 얼마나 빠르게 흐를지 설정

    private float gameTime = 0.0f;

    private int currentMonth = 8;  // 현재 월
    private int currentDay = 0;    // 현재 일

    private void Start()
    {
        // 게임 시작 시 기본 시간 설정 (8월 23일 8시 23분)
        SetTime(5, 23, 8, 23); // 시, 분, 일, 월
    }

    private void Update()
    {
        // 게임 내 시간 업데이트
        gameTime += Time.deltaTime * gameSecondsPerRealSecond;

        // 시간이 24시가 넘어가면 일을 증가시키고 시간을 0으로 초기화
        if (GetHour() >= 24)
        {
            currentDay++;
            gameTime -= 24 * 3600;
        }

        // 예를 들어, 게임 내에서 경과 시간을 흐르게 할 수 있습니다.

        // UI 업데이트 호출
        UpdateUI();
    }

    public float GetGameTimeInSeconds()
    {
        return gameTime;
    }



    public void SetTime(int hour, int minute, int month, int day)
    {
        gameTime = hour * 3600 + minute * 60; // 시간을 초로 변환

        currentDay = day;
        currentMonth = month;

        Debug.Log("SetTime: " + hour + "시 " + minute + "분");

        // UI 업데이트 호출
        UpdateUI();
    }

    public int GetHour()
    {
        return Mathf.FloorToInt(gameTime / 3600);
    }

    public int GetMinute()
    {
        return Mathf.FloorToInt((gameTime % 3600) / 60);
    }

    public int GetDay()
    {
        return currentDay;
    }

    public int GetMonth()
    {
        return currentMonth;
    }

    private void UpdateUI()
    {
        // CalendarUI 스크립트를 가져와서 UI 업데이트 호출
        CalendarUI calendarUI = FindObjectOfType<CalendarUI>();
        if (calendarUI != null)
        {
            calendarUI.UpdateUI();
        }
    }
}
