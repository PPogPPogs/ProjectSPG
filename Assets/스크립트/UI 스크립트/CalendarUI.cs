using UnityEngine;
using UnityEngine.UI;

public class CalendarUI : MonoBehaviour
{
    
    public Text monthText;   // �� �ؽ�Ʈ UI ���
    public Text dayText;     // �� �ؽ�Ʈ UI ���
    public Text hourText;    // �ð� �ؽ�Ʈ UI ���
    public Text minuteText;  // �� �ؽ�Ʈ UI ���
    public CalendarManager calendarManager;

    public void UpdateUI()
    {
        // ���� ���� �� �ð��� ��¥�� �����ͼ� ǥ��
       
        int currentMonth = calendarManager.GetMonth();
        int currentDay = calendarManager.GetDay();
        int currentHour = calendarManager.GetHour();
        int currentMinute = calendarManager.GetMinute();

        // ��, ��, ��, �ð�, ���� �ؽ�Ʈ�� ǥ��
        
        monthText.text = currentMonth + "��";
        dayText.text = currentDay + "��";
        hourText.text = currentHour + "��";
        minuteText.text = currentMinute + "��";
    }

    private void Start()
    {
        UpdateUI();
    }
}
