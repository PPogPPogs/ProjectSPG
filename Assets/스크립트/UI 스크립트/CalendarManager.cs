using UnityEngine;

public class CalendarManager : MonoBehaviour
{
    public float gameSecondsPerRealSecond = 2.0f; // ���� �� �ð��� ���� �ð����� �󸶳� ������ �带�� ����

    private float gameTime = 0.0f;

    private int currentMonth = 8;  // ���� ��
    private int currentDay = 0;    // ���� ��

    private void Start()
    {
        // ���� ���� �� �⺻ �ð� ���� (8�� 23�� 8�� 23��)
        SetTime(5, 23, 8, 23); // ��, ��, ��, ��
    }

    private void Update()
    {
        // ���� �� �ð� ������Ʈ
        gameTime += Time.deltaTime * gameSecondsPerRealSecond;

        // �ð��� 24�ð� �Ѿ�� ���� ������Ű�� �ð��� 0���� �ʱ�ȭ
        if (GetHour() >= 24)
        {
            currentDay++;
            gameTime -= 24 * 3600;
        }

        // ���� ���, ���� ������ ��� �ð��� �帣�� �� �� �ֽ��ϴ�.

        // UI ������Ʈ ȣ��
        UpdateUI();
    }

    public float GetGameTimeInSeconds()
    {
        return gameTime;
    }



    public void SetTime(int hour, int minute, int month, int day)
    {
        gameTime = hour * 3600 + minute * 60; // �ð��� �ʷ� ��ȯ

        currentDay = day;
        currentMonth = month;

        Debug.Log("SetTime: " + hour + "�� " + minute + "��");

        // UI ������Ʈ ȣ��
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
        // CalendarUI ��ũ��Ʈ�� �����ͼ� UI ������Ʈ ȣ��
        CalendarUI calendarUI = FindObjectOfType<CalendarUI>();
        if (calendarUI != null)
        {
            calendarUI.UpdateUI();
        }
    }
}
